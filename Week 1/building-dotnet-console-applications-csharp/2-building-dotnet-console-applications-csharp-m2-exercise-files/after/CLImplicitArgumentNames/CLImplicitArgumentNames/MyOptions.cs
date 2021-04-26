using CommandLine;

namespace CLImplicitArgumentNames
{
    class MyOptions
    {
        [Option("name")]
        public string FirstName { get; set; }
    }
}