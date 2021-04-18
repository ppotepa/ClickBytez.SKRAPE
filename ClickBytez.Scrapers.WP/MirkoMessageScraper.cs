using ClickBytez.SKRAPE.Core.Scraping;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Net.Http;

namespace ClickBytez.Scrapers.WP
{
    public class MirkoMessageScraper : Scraper<MirkoMessageResult>
    {
        private readonly ILogger logger;
        private readonly IConfiguration config;
        private readonly HttpClient httpClient;
        private readonly HtmlDocument document;
        private const string BaseUrl = "https://www.wykop.pl/mikroblog/";
        public override MirkoMessageResult Result { get; protected set; }

        public MirkoMessageScraper(IConfiguration config, ILogger logger, HttpClient httpClient, HtmlDocument document)
        {
            this.logger = logger;
            this.config = config;
            this.httpClient = httpClient;
            this.document = document;
        }

        public async override void Scrape(object @object = null)
        {
            string html = await this.httpClient.GetStringAsync(BaseUrl);
            this.document.LoadHtml(html);
            
            this.Result = new MirkoMessageResult
            {
                Feed = null,
                Id = Guid.NewGuid(),
                Message = "Test message.",
                TimeStamp = DateTime.Now,
                UserName = "PolakAlfa",
                VotesUp = 100
            };

            base.Scrape();
        }
    }
}
