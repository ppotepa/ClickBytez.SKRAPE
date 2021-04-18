using ClickBytez.SKRAPE.Core.Scraping;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ClickBytez.Scrapers.WP
{
    public class MirkoMessageScraper : Scraper<MirkoMessageResult>
    {
        private readonly ILogger logger;
        private readonly IConfiguration config;

        public MirkoMessageScraper(IConfiguration config, ILogger logger) : base(config)
        {
            this.logger = logger;
            this.config = config;
        }

        public override MirkoMessageResult Result { get; protected set; }

        public override void Scrape(object @object = null)
        {
            logger.Information("Scraping for images started.");
        }
    }
}
