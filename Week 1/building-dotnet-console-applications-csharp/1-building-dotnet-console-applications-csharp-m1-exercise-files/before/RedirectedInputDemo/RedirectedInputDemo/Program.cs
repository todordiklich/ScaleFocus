using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedirectedInputDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFileName = Path.Combine(Environment.CurrentDirectory, "names.txt");

            var inputNames = new StreamReader(inputFileName);

            Console.SetIn(inputNames);


            // Output each line of the file until no more lines
            string currentName = Console.ReadLine();            
            while (currentName != null)
            {
                Console.WriteLine("Read from file: " + currentName);

                currentName = Console.ReadLine();
            }


            Console.WriteLine("Press enter to continue");
            
            //Console.SetIn(new StreamReader(Console.OpenStandardInput()));

            Console.ReadLine();
        }
    }
}
