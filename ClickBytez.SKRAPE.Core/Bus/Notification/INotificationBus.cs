using ClickBytez.SKRAPE.Core.Scraping;

namespace ClickBytez.SKRAPE.Core.Bus
{
    public interface INotificationBus
    {
        public void Notify(IScraper scraper);
    }
}
