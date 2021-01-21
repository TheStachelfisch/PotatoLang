using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using PotatoLang.Lexer;

namespace PotatoLang
{
    public class Runtime
    {
        private static string[] _args;
        
        public static void Main(String[] args)
        {
            string test = File.ReadAllText("TestCode.txt");
            
            _args = args.Skip(0).ToArray();
            
            if (_args.Length >= 2)
            {
                Console.WriteLine("Too many files passed, currently only accepts 1 source file");
                Environment.Exit(1);
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            
            var tokens = new Lexer.Lexer(test).Tokenize();
            var parser = new Parser.Parser(tokens).Parse();
            
            watch.Stop();

            foreach (var statement in parser)
            {
                Console.WriteLine(statement + "\n");
            }
            
            Console.WriteLine($"\nExecution took {watch.Elapsed.Seconds} Seconds and {watch.Elapsed.Milliseconds} Milliseconds");
        }
    }
}
