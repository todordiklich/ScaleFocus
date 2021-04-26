using System.Diagnostics;

namespace WebPageReader
{
    class TimedWebPageReaderProcessor : WebPageReaderProcessor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        protected override void PreProcess()
        {
            _stopwatch.Start();

            base.PreProcess();
        }

        protected override void ProcessLine(string line)
        {
            Output.WriteLine(line);
        }

        protected override void PostProcess()
        {
            _stopwatch.Stop();

            Output.WriteLine("Processed in {0}ms", _stopwatch.ElapsedMilliseconds);
        }
    }
}
