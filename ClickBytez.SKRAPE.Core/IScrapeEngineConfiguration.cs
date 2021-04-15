using ClickBytez.SKRAPE.Core;
using Microsoft.Extensions.Configuration;

namespace ClickBytez.SKRAPE.Core.Extensions
{
    public static class IConfigurationExtensions 
    {
        public static ScrapeEngineConfiguration GetSkrapeEngineConfig(this IConfiguration @this)
        {
            return new ScrapeEngineConfiguration
            {
               ScrapersFolder = "scrapers" 
            };
        }
    }
}