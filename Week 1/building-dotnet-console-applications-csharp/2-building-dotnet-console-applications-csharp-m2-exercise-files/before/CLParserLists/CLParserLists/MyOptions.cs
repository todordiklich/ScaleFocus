using System;
using CommandLine;

namespace CLParserLists
{
    class MyOptions
    {
        [OptionArray('n', "names")]
        public string[] Names { get; set; }
    }
}
