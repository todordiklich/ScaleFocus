using System;

namespace WebPageReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new TimedWebPageReaderProcessor();

            p.Process(args, null, Console.Out, Console.Error);
        }
    }
}
