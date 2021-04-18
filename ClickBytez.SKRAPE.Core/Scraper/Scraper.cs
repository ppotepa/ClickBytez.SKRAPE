using Microsoft.Extensions.Configuration;

namespace ClickBytez.SKRAPE.Core.Scraping
{
    public abstract class Scraper<TResult> : IScraper<TResult>
        where TResult : ScrapeResult
    {
        private readonly IConfiguration config;
        public Scraper(IConfiguration configuration)
        {
            this.config = configuration;
        }

        protected string Name => this.GetType().Name;

        public abstract TResult Result { get; protected set; }
        public abstract void Scrape(object @object = null);
    }
}
