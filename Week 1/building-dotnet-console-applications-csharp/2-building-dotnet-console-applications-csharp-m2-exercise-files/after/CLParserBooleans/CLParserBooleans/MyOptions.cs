using CommandLine;

namespace CLParserBooleans
{
    class MyOptions
    {
        [Option('v', "verbose")]
        public bool IsVerbose { get; set; }
    }
}
