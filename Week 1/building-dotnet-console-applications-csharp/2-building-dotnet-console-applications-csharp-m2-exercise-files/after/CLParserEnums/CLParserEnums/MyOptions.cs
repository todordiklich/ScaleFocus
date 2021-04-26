using CommandLine;

namespace CLParserEnums
{
    class MyOptions
    {
        [Option('o')]
        public SortOrder Order { get; set; }
    }
}
