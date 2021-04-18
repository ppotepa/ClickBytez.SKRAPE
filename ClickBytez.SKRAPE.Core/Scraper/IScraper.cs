namespace ClickBytez.SKRAPE.Core.Scraping
{
    public interface IScraper<out TResult> : IScraper where TResult : IScrapeResult
    {
        TResult Result { get; }
    }
    public interface IScraper
    {
        void Scrape(object @object = null);
    }
}