

using Autofac;
using ClickBytez.SKRAPE.Core;
using ClickBytez.SKRAPE.Core.Extensions;
using ClickBytez.SKRAPE.Core.Scraping;
using ClickBytez.Tools.Enumerable;
using ClickBytez.Tools.Scanners;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace ClickBytez.SKRAPE.Engine.Modules
{
    public static class Modules
    {
        static readonly IConfiguration AppConfig = new ConfigurationBuilder().AddJsonFile("skrapesettings.json").Build();

        public class ConfigurationModule : Autofac.Module
        {
            public ConfigurationModule() { }

            protected override void Load(ContainerBuilder builder)
            {
                builder.Register(x => AppConfig)
                    .AsImplementedInterfaces()
                    .OnActivating(e => Console.WriteLine(e.Context.GetHashCode()));

                base.Load(builder);
            }
        }

        public class ProvidersModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder
                    .RegisterType<ScrapersProvider>()
                    .AsImplementedInterfaces()
                    .SingleInstance();

                builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.IsSubclassOf(typeof(Scraper)))
                   .InstancePerDependency()
                   .InstancePerRequest();

                base.Load(builder);
            }
        }

        public class FactoriesModule : Autofac.Module
        {
            public IScrapersProvider Provider { get; set; }
            
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                
                ScrapeEngineConfiguration cfg = AppConfig.GetSkrapeEngineConfig();
                AssemblyScanner scanner = new AssemblyScanner(cfg.ScrapersAbsolutePath);
     
                scanner.Files.ForEach(fileName => 
                {
                    byte[] fileRaw = File.ReadAllBytes(fileName);
                    Assembly assembly = AppDomain.CurrentDomain.Load(fileRaw);

                    builder.RegisterTypes(scanner)
                    .Where(type => type.IsSubclassOf(typeof(Scraper)))
                    .AsSelf();

                });
                

                builder.Register<Func<Type, IScraper>>(context =>
                {
                    //var s = new WPImageScraper(null, null);
                    return scraperType => (IScraper) context.Resolve(scraperType);
                });
            }
        }
    }
}
