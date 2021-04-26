
using CommandLine;

namespace CLParserEnums
{
    class MyOptions
    {
        [Option('o', "order")]
        public SortOrder Order { get; set; }
    }
}
