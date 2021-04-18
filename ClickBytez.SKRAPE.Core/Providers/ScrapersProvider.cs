using ClickBytez.SKRAPE.Core.Extensions;
using ClickBytez.SKRAPE.Core.Scraping;
using ClickBytez.Tools.Assemblies;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClickBytez.SKRAPE.Core.Providers
{
    public class ScrapersProvider : IScrapersProvider
    {
        private readonly ScrapeEngineConfiguration Config;
        private AssemblyScanner<IScraper> scanner;
        private IEnumerable<Assembly> assemblies = null;
        private IEnumerable<byte[]> rawAssemblies = null;
        private IEnumerable<Type> scrapers = null;

        public ScrapersProvider(IConfiguration config)
        {
            this.Config = config.GetSkrapeEngineConfig();
        }
        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (assemblies is null)
                    assemblies = Scanner.Assemblies;
                return assemblies;
            }

            set => assemblies = value;           
        }

        public IEnumerable<byte[]> RawAssemblies => throw new NotImplementedException();

        public IEnumerable<Type> Scrapers
        {
            get
            {
                if (scrapers is null)
                    scrapers = Scanner.FoundTypes;
                return scrapers;
            }
            set => scrapers = value;
        }

        private AssemblyScanner<IScraper> Scanner
        {
            get 
            {
                if (scanner is null) 
                    scanner = new AssemblyScanner<IScraper>(this.Config.ScrapersAbsolutePath);
                return scanner;
            }

            set => scanner = value;
           
        }
    }

}