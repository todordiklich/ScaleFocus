using System;

namespace CLParserDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new MyOptions();

            CommandLine.Parser.Default.ParseArgumentsStrict(args, options, OnFail);

            for (int i = 0; i < options.Times; i++)
            {
                Console.WriteLine(options.Message);
            }

            Console.ReadLine();
        }

        private static void OnFail()
        {
            Console.WriteLine("Sorry something went wrong...");

            Console.ReadLine();

            Environment.Exit(-1);
        }
    }
}
