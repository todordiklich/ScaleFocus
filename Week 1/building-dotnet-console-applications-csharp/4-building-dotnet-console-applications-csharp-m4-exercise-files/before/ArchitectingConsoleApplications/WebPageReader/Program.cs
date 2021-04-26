using System;

namespace WebPageReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new WebPageReaderProcessor();

            p.Process(args, null, Console.Out, Console.Error);
        }
    }
}
