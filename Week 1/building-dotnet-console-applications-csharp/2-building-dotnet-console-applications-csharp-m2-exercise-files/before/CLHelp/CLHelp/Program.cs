using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHelp
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new MyOptions();

            CommandLine.Parser.Default.ParseArgumentsStrict(args, options, OnFail);
        }

        private static void OnFail()
        {
            Console.ReadLine();

            Environment.Exit(-1);            
        }
    }
}
