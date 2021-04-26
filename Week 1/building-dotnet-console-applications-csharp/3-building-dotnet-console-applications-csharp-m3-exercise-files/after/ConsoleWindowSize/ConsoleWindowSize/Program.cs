using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWindowSize
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.ReadLine();

            Console.SetWindowSize(20, 20);
            Console.SetBufferSize(20, 20);

            Console.ReadLine();
        }
    }
}
