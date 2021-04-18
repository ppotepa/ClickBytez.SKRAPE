namespace ClickBytez.SKRAPE.Core.Scraping
{
    public class ScrapingCompletedEventArgs
    {
        public ScrapingCompletedEventArgs(ScrapeResult result) { Result = result; }
        public object Result { get; }
    }
}
