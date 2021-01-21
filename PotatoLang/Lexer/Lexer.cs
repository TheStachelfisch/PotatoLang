using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace PotatoLang.Lexer
{
    public class Lexer
    {
        private readonly string _fixedSourceInput;
        private int _currentPosition;
        
        private char CurrentCharacter => _fixedSourceInput[_currentPosition];

        public Lexer(string sourceInput) => _fixedSourceInput = new PreLexer(sourceInput).RemoveUnnecessaryTokens();
        

        public Token[] Tokenize()
        {
            List<Token> tokens = new List<Token>();
            
            tokens.Add(new Token(TokenType.BeginningOfFile));
            
            // Make this better at some point
            string ReadNextToken()
            {
                StringBuilder currentText = new StringBuilder();
                
                while (char.IsWhiteSpace(CurrentCharacter))
                    _currentPosition++;

                while (!(_currentPosition >= _fixedSourceInput.Length) && !char.IsWhiteSpace(CurrentCharacter))
                {
                    currentText.Append(CurrentCharacter);

                    if (CurrentCharacter == '"')
                    {
                        _currentPosition++;

                        while (CurrentCharacter != '"')
                        {
                            currentText.Append(CurrentCharacter);
                            _currentPosition++;
                        }
                        
                        currentText.Append(CurrentCharacter);
                        _currentPosition++;
                        
                        break;
                    }

                    _currentPosition++;
                }

                return currentText.ToString();
            }

            while (!(_currentPosition >= _fixedSourceInput.Length))
            {
                string word = ReadNextToken();
                object tokenValue = null;
                
                TokenType type = word switch
                {
                    "TextPotato" => TokenType.TextPotato,
                    "NumberPotato" => TokenType.NumberPotato,
                    "=" => TokenType.Is,
                    "Shout" => TokenType.Shout,
                    "Listen" => TokenType.Listen,
                    _ => TokenType.Undefined
                };

                if (type == TokenType.Undefined)
                {
                    if (Int64.TryParse(word, out Int64 result))
                    {
                        type = TokenType.Number;
                        tokenValue = result;
                    }
                    else if (word.StartsWith('"') && word.EndsWith('"'))
                    {
                        type = TokenType.String;
                        tokenValue = word.Replace('"', Char.MinValue);
                    }
                    else
                    {
                        type = TokenType.Identifier;
                        tokenValue = word;
                    }
                }
                
                tokens.Add(new Token(type, tokenValue));
            }

            tokens.Add(new Token(TokenType.EndOfFile));
            return tokens.ToArray();
        }
    }
}