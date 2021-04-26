using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleColors
{
    class Program
    {
        static void Main(string[] args)
        {
            var origColor = Console.ForegroundColor;

            Console.WriteLine("Pluralsight");
            Console.WriteLine("Rocks");


            WriteWarning("This is a warning...");
            WriteError("This is an error");

            Console.ReadLine();

            Console.ForegroundColor = origColor;
        }











        static void WriteWarning(string s)
        {
            var origColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(s);

            Console.ForegroundColor = origColor;
        }

        static void WriteError(string s)
        {
            var origColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);

            Console.ForegroundColor = origColor;
        }
    }
}
