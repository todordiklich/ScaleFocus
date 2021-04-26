using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {

                var isCtrlC = e.SpecialKey == ConsoleSpecialKey.ControlC;
                var isCtrlBreak = e.SpecialKey == ConsoleSpecialKey.ControlBreak;

                // Prevent CTRL-C from terminating
                if (isCtrlC)
                {
                    e.Cancel = true;
                }

                // e.Cancel defaults to false so CTRL-BREAK will still cause termination

            };       

            while (true){}
            
        }
    }
}
