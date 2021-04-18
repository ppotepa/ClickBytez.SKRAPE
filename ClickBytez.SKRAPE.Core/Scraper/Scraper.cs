using Autofac;
using ClickBytez.SKRAPE.Core.Bus;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ClickBytez.SKRAPE.Core.Scraping
{
    public abstract class Scraper<TResult> : IScraper<TResult>, IDisposable
        where TResult : ScrapeResult
    {
        private static Dictionary<Type, int> Instances = new Dictionary<Type, int>();
        private readonly Type scraperType;
        private bool disposed = false;
        private SafeHandle safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        private IComponentContext context;

        public IScraper UseContext(IComponentContext context) 
        {
            this.context = context;
            return this;
        }
        
        public Scraper()
        {
            this.scraperType = this.GetType();
            bool isNewInstance = Instances.TryAdd(scraperType, 1);
            if (isNewInstance is false)
            {
                Instances[scraperType]++;
            }
        }

        protected string Name => scraperType.Name;
        public abstract TResult Result { get; protected set; }
        object IScraper.Result => Result;
        protected int CurrentScraperInstancesCount => Instances[scraperType];

        public void Dispose() => Dispose(true);
        
        public virtual void Scrape(object @object = null)
        {
            INotificationBus bus = this.context.Resolve<INotificationBus>();
            bus.Notify(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) 
            {
                return;
            }
          
            if (disposing) 
            {
                Instances[scraperType]--;
            }

            disposed = true;
        }
    }
}
