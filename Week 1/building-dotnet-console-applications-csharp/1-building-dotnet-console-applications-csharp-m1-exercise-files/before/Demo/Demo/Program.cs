using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                // While no one is pressing any keys
                while (!Console.KeyAvailable)
                {
                    // keep doing nothing
                }


                // Key has been pressed so read it
                var k = Console.ReadKey(true);

                if (Console.CapsLock && Console.NumberLock)
                {
                    Console.WriteLine(k.KeyChar);
                }

                switch (k.Key)
                {
                    case ConsoleKey.UpArrow:
                        Console.WriteLine("Up arrow was pressed");
                        break;
                    case ConsoleKey.DownArrow:
                        Console.WriteLine("Down arrow was pressed");
                        break;
                    case ConsoleKey.RightArrow:
                        Console.WriteLine("Right arrow was pressed");
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.WriteLine("Left arrow was pressed");
                        break;
                }
            } while (true);
        }
    }
}
