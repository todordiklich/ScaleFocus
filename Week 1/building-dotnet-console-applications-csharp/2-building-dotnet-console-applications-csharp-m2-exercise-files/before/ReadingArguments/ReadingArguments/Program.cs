using System;

namespace ReadingArguments
{
    class Program
    {
        static void Main(string[] args)
        {
            var fullCommandLineString = Environment.CommandLine;

            Console.WriteLine("Full command line string:");
            Console.WriteLine(fullCommandLineString);

            
        }
    }
}
