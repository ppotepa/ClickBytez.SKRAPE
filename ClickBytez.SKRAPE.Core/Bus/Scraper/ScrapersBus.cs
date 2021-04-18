using ClickBytez.SKRAPE.Core.Providers;
using ClickBytez.SKRAPE.Core.Scraping;
using ClickBytez.Tools.Extensions.Enumerable;
using Serilog;
using System;

namespace ClickBytez.SKRAPE.Core.Bus
{
    public class ScrapersBus : IScrapersBus
    {
        private readonly ILogger logger;
        private readonly Func<Type, IScraper> scrapers;
        private readonly IScrapersProvider provider;

        public ScrapersBus(IScrapersProvider provider, Func<Type, IScraper> scrapers, ILogger logger)
        {
            this.logger = logger;
            this.scrapers = scrapers;
            this.provider = provider;
        }

        public void Run()
        {
            this.provider.Scrapers.ForEach(currentScraper =>
            {
                IScraper instance = scrapers(currentScraper);
                instance.Scrape();
                this.logger.Information("Running scraper {A}", currentScraper);
            });
        }
    }
}
