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
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;


            Console.WriteLine("Pluralsight");
            Console.WriteLine("Rocks");


            Console.ReadLine();
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
