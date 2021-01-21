using System;
using System.Collections.Generic;
using System.Xml;
using PotatoLang.Lexer;

namespace PotatoLang.Parser
{
    public class Parser
    {
        private readonly Token[] _tokens;
        private int _currentPosition;

        public Parser(Token[] tokens) => _tokens = tokens;

        public Statement[] Parse()
        {
            List<Statement> statements = new List<Statement>();

            while (PeekNextToken().TokenType != TokenType.EndOfFile)
            {
                Token token = NextToken();

                switch (token.TokenType)
                {
                    // This doesn't feel right
                    case TokenType.Is:
                        VerifyRemainingTokens(1, "Unexpected end of Input at '='");

                        Token identifier = PeekLastToken();

                        if (identifier.TokenType != TokenType.Identifier)
                            Error($"Expected identifier before '=' Value: {identifier.ToString()}");

                        Token operand = NextToken();

                        if (operand.TokenType == TokenType.Identifier || operand.TokenType == TokenType.Number || operand.TokenType == TokenType.String || operand.TokenType == TokenType.Listen)
                        {
                            statements.Add(new Statement(StatementType.Is, identifier, operand));
                            break;
                        }
                        
                        Error($"Expected identifier after '=' Value: {operand.ToString()}");
                        break;
                }
            }

            return statements.ToArray();
        }

        private Token PeekNextToken() => _tokens[_currentPosition + 1];
        private Token PeekLastToken() => _tokens[_currentPosition - 2];
        private Token NextToken() => _tokens[_currentPosition++];

        public void VerifyRemainingTokens(int amount, string errorMessage)
        {
            if (_currentPosition + amount >= _tokens.Length)
                Error(errorMessage);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
            Environment.Exit(1);
        }
    }
}