using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorLua
{
    internal class Lexer
    {
        private string source;
        private int currentPosition;

        public Lexer(string source)
        {
            this.source = source;
            currentPosition = 0;
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (currentPosition < source.Length)
            {
                char currentChar = source[currentPosition];

                if (char.IsWhiteSpace(currentChar))
                {
                    currentPosition++;
                    continue;
                }
                else if (char.IsDigit(currentChar))
                {
                    tokens.Add(ReadNumber());
                }
                else if (char.IsLetter(currentChar))
                {
                    tokens.Add(ReadIdentifier());
                }
                else if (currentChar == '+')
                {
                    tokens.Add(new Token { Type = "Plus", Value = "+" });
                    currentPosition++;
                }
                else if (currentChar == '-')
                {
                    tokens.Add(new Token { Type = "Minus", Value = "-" });
                    currentPosition++;
                }
                else if (currentChar == '*')
                {
                    tokens.Add(new Token { Type = "Multiply", Value = "*" });
                    currentPosition++;
                }
                else if (currentChar == '/')
                {
                    tokens.Add(new Token { Type = "Divide", Value = "/" });
                    currentPosition++;
                }
                else
                {
                    throw new Exception($"Unknown character: {currentChar}");
                }
            }

            return tokens;
        }

        private Token ReadNumber()
        {
            int start = currentPosition;
            while (currentPosition < source.Length && char.IsDigit(source[currentPosition]))
            {
                currentPosition++;
            }

            return new Token
            {
                Type = "Number",
                Value = source.Substring(start, currentPosition - start)
            };
        }

        private Token ReadIdentifier()
        {
            int start = currentPosition;
            while (currentPosition < source.Length && char.IsLetterOrDigit(source[currentPosition]))
            {
                currentPosition++;
            }

            return new Token
            {
                Type = "Identifier",
                Value = source.Substring(start, currentPosition - start)
            };
        }
    }
}