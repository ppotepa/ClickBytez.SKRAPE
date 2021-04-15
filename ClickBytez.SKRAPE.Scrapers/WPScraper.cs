using ClickBytez.SKRAPE.Engine.Scraping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ClickBytez.SKRAPE.Scrapers.WP
{
    public class WPScraper : Scraper
    {
        public WPScraper(Logger<Scraper> logger, IConfiguration config) : base(logger, config)
        {
            
        }

        protected override void OnBeforeScraping()
        {
            base.OnBeforeScraping();
        }

        protected override void OnScrapingCompleted()
        {
            base.OnScrapingCompleted();
        }

        protected override void Scrape()
        {
            base.Scrape();
        }
    }
}
