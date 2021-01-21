namespace PotatoLang.Lexer
{
    public struct Token
    {
        public readonly TokenType TokenType;
        public readonly object Value;

        public Token(TokenType type)
        {
            TokenType = type;
            Value = null;
        }

        public Token(TokenType type, object value)
        {
            TokenType = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{TokenType} : {Value ?? ""}";
        }
    }
}