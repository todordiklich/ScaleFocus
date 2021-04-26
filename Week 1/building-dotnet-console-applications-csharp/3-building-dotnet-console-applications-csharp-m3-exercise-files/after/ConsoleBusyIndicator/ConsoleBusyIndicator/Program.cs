using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleBusyIndicator
{
    class Program
    {
        static void Main(string[] args)
        {
            var busy = new ConsoleBusyIndicator();

            var files = Enumerable.Range(1, 100).Select(n => "File" + n +".txt");

            Console.CursorVisible = false;

            foreach (var file in files)
            {
                Thread.Sleep(100); // simulate some work

                busy.UpdateProgress();
            }

            Console.CursorVisible = true;

        }
    }
}
