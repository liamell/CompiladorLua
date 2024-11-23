using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorLua
{
    public enum TokenType
    {

        Keyword,
        Identifier,
        Operator,
        ParenthesisOpen,
        ParenthesisClose,
        Number,
        EndOfFile,
        Plus,        
        Minus,        
        Multiply,     
        Divide
    }

  
    public class Lexer
    {
        private string input;
        private int position;
        private int readPosition;
        private char currentChar;

        public Lexer(string input)
        {
            this.input = input;
            this.position = 0;
            this.readPosition = 0;
            this.currentChar = input.Length > 0 ? input[0] : '\0';
        }

        private void Advance()
        {
            position = readPosition;
            readPosition++;

            if (readPosition >= input.Length)
            {
                currentChar = '\0';  // End of input
            }
            else
            {
                currentChar = input[readPosition];
            }
        }

        private bool IsLetter(char c)
        {
            return char.IsLetter(c) || c == '_';
        }

        private bool IsDigit(char c)
        {
            return char.IsDigit(c);
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (currentChar != '\0')
            {
                if (char.IsWhiteSpace(currentChar))
                {
                    Advance();
                    continue;
                }

                if (IsLetter(currentChar))
                {
                    string identifier = ReadIdentifier();
                    tokens.Add(new Token(TokenType.Identifier, identifier));
                    continue;
                }

                if (IsDigit(currentChar))
                {
                    string literal = ReadLiteral();
                    tokens.Add(new Token(TokenType.Number, literal));
                    continue;
                }

                if (IsOperator(currentChar))
                {
                    string operatorValue = currentChar.ToString();
                    tokens.Add(new Token(TokenType.Operator, operatorValue));
                    Advance();
                    continue;
                }

                switch (currentChar)
                {
                    case '(':
                        tokens.Add(new Token(TokenType.ParenthesisOpen, "("));
                        Advance();
                        break;
                    case ')':
                        tokens.Add(new Token(TokenType.ParenthesisClose, ")"));
                        Advance();
                        break;
                    default:
                        throw new Exception($"Unknown character: {currentChar}");
                }
            }

            // Agregar el token de EOF
            tokens.Add(new Token(TokenType.EndOfFile, ""));
            return tokens;
        }

        private string ReadIdentifier()
        {
            int startPos = position;
            while (IsLetter(currentChar) || IsDigit(currentChar))
            {
                Advance();
            }
            return input.Substring(startPos, position - startPos);
        }

        private string ReadLiteral()
        {
            int startPos = position;
            while (IsDigit(currentChar))
            {
                Advance();
            }
            return input.Substring(startPos, position - startPos);
        }
    }


}



