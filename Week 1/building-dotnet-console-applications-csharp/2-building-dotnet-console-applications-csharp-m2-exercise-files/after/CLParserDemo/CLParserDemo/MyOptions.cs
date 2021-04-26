using CommandLine;

namespace CLParserDemo
{
    class MyOptions
    {
        [Option('m', "message", Required=true)]
        public string Message { get; set; }

        [Option('t', "times")]
        public int Times { get; set; }
    }
}
