using System.IO;
using System.Net;
using ProcessorTemplate;

namespace WebPageReader
{
    class WebPageReaderProcessor : ProcessorTemplateBase<WebPageReaderOptions>
    {
        protected override void PreProcess()
        {
            Output.WriteLine("Downloading page...");

            var wc = new WebClient();

            var page = wc.DownloadString(Options.Uri);

            Input = new StringReader(page);
        }

        protected override void ProcessLine(string line)
        {
            Output.WriteLine(line);
        }
    }
}
