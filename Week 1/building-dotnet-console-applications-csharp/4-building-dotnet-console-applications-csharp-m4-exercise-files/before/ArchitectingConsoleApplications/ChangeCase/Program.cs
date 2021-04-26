using System;

namespace ChangeCase
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new ChangeCaseProcessor();

            p.Process(args, Console.In, Console.Out, Console.Error);
        }
    }
}
