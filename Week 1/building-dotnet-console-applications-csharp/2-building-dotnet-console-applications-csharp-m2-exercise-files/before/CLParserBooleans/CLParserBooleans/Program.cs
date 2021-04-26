using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLParserBooleans
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new MyOptions();

            CommandLine.Parser.Default.ParseArguments(args, options);

            if (options.IsVerbose)
            {
                Console.WriteLine("Running in verbose mode");
            }
            else
            {
                Console.WriteLine("Running in short mode");
            }

            Console.ReadLine();
        }
    }
}
