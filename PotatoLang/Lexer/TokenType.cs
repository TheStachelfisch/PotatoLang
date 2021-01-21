namespace PotatoLang.Lexer
{
    public enum TokenType
    {
        BeginningOfFile,
        EndOfFile,
        
        String,
        Number,
        Identifier,
        Is,
        Undefined,

        TextPotato, // Basic string
        NumberPotato, // Basic int
        
        Shout, // Console.WriteLine()
        Listen, // Console.ReadLine()
    }
}