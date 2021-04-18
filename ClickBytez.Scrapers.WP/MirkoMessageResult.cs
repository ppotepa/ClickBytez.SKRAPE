using ClickBytez.SKRAPE.Core.Scraping;
using System;

namespace ClickBytez.Scrapers.WP
{
    public class MirkoMessageResult : ScrapeResult
    {
        public DateTime TimeStamp {get;set;}
        public Guid Id {get;set;}
        public string Message {get;set;}
        public string UserName {get;set;}
        public int VotesUp {get;set;}

        public MirkoMessageResult[] Feed { get; set; }
    }
}