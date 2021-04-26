using System;
using CommandLine;

namespace ProcessorTemplate
{
    public sealed class ActionProcessor<TOptions> where TOptions : new()
    {
        public TOptions Options { get; set; }

        public Func<bool> ValidateArguments { get; set; }
        public Action PreProcess { get; set; }
        public Action<string> ProcessLine { get; set; }
        public Action PostProcess { get; set; }


        public void Process(string[] args)
        {
            ParseOptions(args);


            bool isValidArguments = ValidateArguments == null || ValidateArguments();


            if (isValidArguments)
            {
                if (PreProcess != null)
                {
                    PreProcess();
                }


                ProcessLines();


                if (PostProcess != null)
                {
                    PostProcess();
                }
            }
        }

        private void ParseOptions(string[] args)
        {
            Options = new TOptions();

            Parser.Default.ParseArgumentsStrict(args, Options);
        }

        private void ProcessLines()
        {
            var currentLine = Console.ReadLine();

            while (currentLine != null)
            {
                if (ProcessLine != null)
                {
                    ProcessLine(currentLine);
                }

                currentLine = Console.ReadLine();
            }
        }
    }
}