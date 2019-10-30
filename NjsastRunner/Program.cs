using System;
using System.IO;
using Njsast.Compress;
using Njsast.Reader;

namespace NjsastRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = args[0];
            var input = File.ReadAllText(fileName);
            var toplevel = new Parser(Options.GetOptions(new Options()), input).Parse();
            toplevel.FigureOutScope();
            toplevel = toplevel.Compress(new CompressOptions
            {
                MaxPasses = 2,
                EnableBlockElimination = true,
                EnableBooleanCompress = true,
                EnableVariableHoisting = true,
                EnableEmptyStatementElimination = true,
                EnableFunctionReturnCompress = true,
                EnableUnreachableCodeElimination = true,
                EnableUnusedFunctionElimination = true
            });
            toplevel.Mangle();
            var result = toplevel.PrintToString();
            Console.Write(result);
        }
    }
}