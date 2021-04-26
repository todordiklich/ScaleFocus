using CommandLine;

namespace WebPageReader
{
    class WebPageReaderOptions
    {
        [Option('u', "uri")]
        public string Uri{ get; set; }
    }
}