using System;

namespace CLParserLists
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new MyOptions();

            CommandLine.Parser.Default.ParseArguments(args, options);

            foreach (var n in options.Names)
            {
                Console.WriteLine(n);
            }

            Console.ReadLine();
        }
    }
}
