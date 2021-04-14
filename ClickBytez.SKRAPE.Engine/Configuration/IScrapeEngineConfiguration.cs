using Microsoft.Extensions.Configuration;
using System.IO;

namespace ClickBytez.SKRAPE.Engine
{
    public static class IConfigurationExtensions 
    {
        public static ScrapeEngineConfiguration ScrapeEngineConfig(this IConfiguration @this)
        {
            return new ScrapeEngineConfiguration
            {
               ScrapersRelativePath = "./scrapers" 
            };
        }
    }

    public class ScrapeEngineConfiguration 
    {
        public readonly string BaseDirectory = Directory.GetCurrentDirectory();
        public string ScrapersRelativePath { get; set; }
    }
}