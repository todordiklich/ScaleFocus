using System;
using System.IO;
using ProcessorTemplate;

namespace ChangeCase
{
    class ChangeCaseProcessor : ProcessorTemplateBase<ChangeCaseOptions>
    {
        private int _linesProcessed;

        protected override void PreProcess()
        {
            Output.WriteLine("Converting to: " + Options.TargetCase);
        }

        protected override void ProcessLine(string line)
        {
            _linesProcessed++;

            switch (Options.TargetCase)
            {
                case Case.Upper:
                    Output.WriteLine(line.ToUpper());
                    break;
                case Case.Lower:
                    Output.WriteLine(line.ToLower());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void PostProcess()
        {
            Output.WriteLine("{0} lines processed.", _linesProcessed);
        }
    }
}
