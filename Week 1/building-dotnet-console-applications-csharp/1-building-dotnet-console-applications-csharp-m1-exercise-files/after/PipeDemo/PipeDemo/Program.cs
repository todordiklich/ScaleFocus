using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string s;

            s = Console.ReadLine();
            while (s != null)
            {
                Console.WriteLine(Reverse(s));

                s = Console.ReadLine();
            }
        }


        private static string Reverse(string s)
        {
            var a = s.ToCharArray();
            Array.Reverse(a);
            return new string(a);
        }

    }
}
