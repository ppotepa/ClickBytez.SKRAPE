using ClickBytez.SKRAPE.Core.Scraping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ClickBytez.Scrapers.WP
{
    public class WPImageScraper : Scraper
    {
        public WPImageScraper(Logger<Scraper> logger, IConfiguration config) : base(logger, config)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
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
