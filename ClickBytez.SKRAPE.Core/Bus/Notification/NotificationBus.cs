using ClickBytez.SKRAPE.Core.Scraping;
using ClickBytez.Tools.Extensions.Object;
using Serilog;

namespace ClickBytez.SKRAPE.Core.Bus
{
    public class NotificationBus : INotificationBus
    {
        private readonly ILogger logger;

        public NotificationBus(ILogger logger)
        {
            this.logger = logger;
        }

        public void Notify(IScraper scraper)
        {
            this.logger.Information("Notification bus obtained : {A}", scraper);
            this.logger.Information("Notification Result : {A}", scraper.Result.ToJson(true));
        }
    }
}
