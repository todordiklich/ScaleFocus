using CommandLine;
using CommandLine.Text;

namespace CLHelp
{
    class MyOptions
    {
        [Option('n', "name", Required=true)]
        public string Name { get; set; }

        [Option('a', "age")]
        public int Age { get; set; }

        //[HelpOption]
        //public string GetUsage()
        //{
        //    return "Something's wrong here";
        //}
    }
}
