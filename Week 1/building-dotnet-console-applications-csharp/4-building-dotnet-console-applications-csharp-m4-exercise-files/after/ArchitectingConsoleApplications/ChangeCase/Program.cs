using System;
using ProcessorTemplate;

namespace ChangeCase
{
    class Program
    {
        static void Main(string[] args)
        {
            //var p = new ChangeCaseProcessor();

            //p.Process(args, Console.In, Console.Out, Console.Error);


            var p = new ActionProcessor<ChangeCaseOptions>();

            p.PreProcess = () => Console.WriteLine(p.Options.TargetCase);

            p.ProcessLine = (line) =>
                            {
                                switch (p.Options.TargetCase)
                                {
                                    case Case.Upper:
                                        Console.WriteLine(line.ToUpper());
                                        break;
                                    case Case.Lower:
                                        Console.WriteLine(line.ToLower());
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }
                            };

            p.PostProcess = () => Console.WriteLine("FIN");

            p.Process(args);

        }
    }
}
