using System;
using CommandLine;

namespace CLParserLists
{
    class MyOptions
    {
        [OptionArray('n', "names", DefaultValue = new string[]{})]
        public string[] Names { get; set; }
    }
}
