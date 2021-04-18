using ClickBytez.SKRAPE.Core;
using ClickBytez.SKRAPE.Core.Extensions;
using ClickBytez.SKRAPE.Core.Scraping;
using ClickBytez.Tools.Enumerable;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;

namespace ClickBytez.SKRAPE.Engine
{
    public sealed class SkrapeEngine : ISkrapeEngine
    {
        public readonly IScrapersProvider ScrapersProvider;
        private readonly ScrapeEngineConfiguration Config;
        private readonly Func<Type, IScraper> getScraper;
        private int Ticks = 0;

        public SkrapeEngine(IConfiguration configuration, IScrapersProvider scrapersProvider, Func<Type, IScraper> factory)
        {
            Config = configuration.GetSkrapeEngineConfig();
            ScrapersProvider = scrapersProvider;
            getScraper = factory;
        }

        public bool Initialized { get; private set; }
        public bool IsRunning { get; internal set; }

        public SkrapeEngine Initialize() 
        {
            Initialized = true;
            return this;
        }

        public bool Start() 
        {
            if (Initialized is false)
                throw new SkrapeEngineNotInitializedException("SKRAPE was not initialized.");

            IsRunning = true;

            return ThreadPool.QueueUserWorkItem(this.MainThread);
        }

        private void MainThread(object stateInfo) 
        {
            while (IsRunning)
            {
                ScrapersProvider.Scrapers.ForEach(scraper => 
                {
                    IScraper instance = this.getScraper(scraper) as IScraper;
                    ThreadPool.QueueUserWorkItem(instance.Scrape);
                });

                Thread.Sleep(100);
                Console.WriteLine($"Ticks : {Ticks++}");
            }
           
        }
    }
}