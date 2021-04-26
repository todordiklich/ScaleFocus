using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTitle
{
    class Program
    {
        static void Main(string[] args)
        {

            for (var percentComplete = 0; percentComplete <= 100; percentComplete++)
            {
                var title = string.Format("{0}% Complete", percentComplete);

                Console.Title = title;

                // simulate some work being done
                Thread.Sleep(100);

            }
            
            Console.ReadLine();
        }
    }
}
