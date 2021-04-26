using CommandLine;

namespace CLVerbArguments
{
    class CookVerbSubOptions
    {
        [Option("mins")]
        public int CookingMinutes { get; set; }

        [Option("temp")]
        public int CookingTemperature { get; set; }
    }
}