using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorLua
{
    public class Parser
    {
        private List<Token> tokens;
        private int current;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            current = 0;
        }

        public AstNode ParseExpression()
        {
            var left = ParseTerm();

            while (Match("Plus", "Minus"))
            {
                string operatorSymbol = Previous().Value;
                var right = ParseTerm();
                left = new BinaryExpression
                {
                    Left = left,
                    Operator = operatorSymbol,
                    Right = right
                };
            }

            return left;
        }

        private AstNode ParseTerm()
        {
            var left = ParseFactor();

            while (Match("Multiply", "Divide"))
            {
                string operatorSymbol = Previous().Value;
                var right = ParseFactor();
                left = new BinaryExpression
                {
                    Left = left,
                    Operator = operatorSymbol,
                    Right = right
                };
            }

            return left;
        }

        private AstNode ParseFactor()
        {
            if (Match("Number"))
            {
                return new NumberLiteral { Value = Previous().Value };
            }

            throw new Exception("Unexpected token");
        }

        private bool Match(params string[] types)
        {
            if (IsAtEnd()) return false;

            foreach (var type in types)
            {
                if (Check(type))
                {
                    Advance();
                    return true;
                }
            }

            return false;
        }

        private bool Check(string type)
        {
            if (IsAtEnd()) return false;
            return Peek().Type == type;
        }

        private Token Advance()
        {
            if (!IsAtEnd()) current++;
            return Previous();
        }

        private bool IsAtEnd()
        {
            return current >= tokens.Count;
        }

        private Token Peek()
        {
            return tokens[current];
        }

        private Token Previous()
        {
            return tokens[current - 1];
        }
    }

}
