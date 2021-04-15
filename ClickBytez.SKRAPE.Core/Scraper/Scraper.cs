using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace ClickBytez.SKRAPE.Core.Scraping
{
    public abstract class Scraper : IScraper
    {
        protected readonly Logger<Scraper> logger;
        private readonly IConfiguration config;

        protected string Name => this.GetType().Name;

        public Scraper(Logger<Scraper> logger, IConfiguration config)
        {
            this.logger = logger;
            this.config = config;
        }

        protected virtual void OnBeforeScraping() 
        {
          
        }

        protected virtual void OnScrapingCompleted() => throw new NotImplementedException();
        protected virtual void Scrape() => throw new NotImplementedException();

        public void Start()
        {
            this.OnBeforeScraping();
            this.Scrape();
            this.OnScrapingCompleted();
        }
    }
}
