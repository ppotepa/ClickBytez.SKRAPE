using System.IO;

namespace ClickBytez.SKRAPE.Core
{
    public class ScrapeEngineConfiguration 
    {
        public readonly string BaseDirectory = Directory.GetCurrentDirectory();
        public string ScrapersFolder { get; set; }
        public string ScrapersAbsolutePath => Path.Combine(BaseDirectory, ScrapersFolder);
    }
}