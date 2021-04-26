namespace CLVerbArguments
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new MyOptions();

            CommandLine.Parser.Default.ParseArguments(args, options, OnVerbCommand);
        }

        private static void OnVerbCommand(string verbName, object verbSubOptions)
        {
            switch (verbName)
            {
                case "mix":
                    var mixSubOptions = (MixVerbSubOptions) verbSubOptions;
                    break;
                case "cook":
                    var cookSubOptions = (CookVerbSubOptions) verbSubOptions;
                    break;
            }

        }
    }
}
