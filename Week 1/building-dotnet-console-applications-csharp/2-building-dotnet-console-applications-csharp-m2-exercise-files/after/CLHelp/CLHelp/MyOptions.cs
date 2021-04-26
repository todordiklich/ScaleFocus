using CommandLine;
using CommandLine.Text;

namespace CLHelp
{
    class MyOptions
    {
        [Option('n', "name", Required=true, HelpText = "The name of the person")]
        public string Name { get; set; }

        [Option('a', "age", HelpText = "The person's age")]
        public int Age { get; set; }

        [ParserState]
        public IParserState ParserState { get; set; }

        //[HelpOption]
        //public string GetUsage()
        //{
        //    return HelpText.AutoBuild(this);
        //}
    }
}
