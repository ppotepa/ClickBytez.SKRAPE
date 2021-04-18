using Autofac;

namespace ClickBytez.SKRAPE.Core.Scraping
{
    public interface IScraper<TResult> : IScraper where TResult : IScrapeResult
    {
        new TResult Result { get; }
    }

    public interface IScraper
    {
        void Scrape(object @object = null);
        object Result { get; }
    }
}