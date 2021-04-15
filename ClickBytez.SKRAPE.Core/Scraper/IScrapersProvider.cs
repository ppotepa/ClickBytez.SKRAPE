using ClickBytez.SKRAPE.Core.Extensions;
using ClickBytez.Tools.Scanners;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClickBytez.SKRAPE.Core.Scraping
{
    public interface IScrapersProvider
    {
        IEnumerable<Type> Scrapers { get; }
        IEnumerable<Assembly> Assemblies { get; }
        IEnumerable<byte[]> RawAssemblies { get; }
    }

    public class ScrapersProvider : IScrapersProvider
    {
        public ScrapersProvider(IConfiguration config)
        {
            this.Config = config.GetSkrapeEngineConfig();
        }

        private IEnumerable<Type> scrapers = null;
        private IEnumerable<Assembly> assemblies = null;
        private IEnumerable<byte[]> rawAssemblies = null;

        private readonly ScrapeEngineConfiguration Config;

        public IEnumerable<Type> Scrapers
        {
            get 
            {
                if(scrapers is null)
                    scrapers = new AssemblyScanner(Config.ScrapersAbsolutePath).ScanForTypes<Scraper>().ToArray();
                return scrapers;
            }
            set 
            {
                scrapers = value;
            }
        }

        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (assemblies is null)
                    assemblies = new AssemblyScanner(Config.ScrapersAbsolutePath).ScanWithSubclass<Scraper>().ToArray();
                return assemblies;
            }
            set
            {
                assemblies = value;
            }
        }

        public IEnumerable<byte[]> RawAssemblies
        {
            get
            {
                if (rawAssemblies is null)
                    rawAssemblies = new AssemblyScanner(Config.ScrapersAbsolutePath).GetRaw().ToArray();
                return rawAssemblies;
            }
            set
            {
                rawAssemblies = value;
            }
        }
    }

}