using CommandLine;

namespace ChangeCase
{  
    class ChangeCaseOptions
    {
        [Option('c', "case", Required = true, HelpText = "upper / lower")]
        public Case TargetCase { get; set; }
    }
}
