using System;
using System.Text.RegularExpressions;

namespace PotatoLang.Lexer
{
    /// <summary>
    /// The Pre Lexer removes unneeded tokens like Comments
    /// </summary>
    internal class PreLexer
    {
        private string _input = "";
        
        internal PreLexer(String input) { _input = input; }

        internal string RemoveUnnecessaryTokens() => Regex.Replace(_input, @"//(.*?)\r?\n", "\n");
    }
}