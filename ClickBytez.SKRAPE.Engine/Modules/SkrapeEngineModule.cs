using Autofac;
using AutofacSerilogIntegration;
using ClickBytez.SKRAPE.Core.Bus;
using ClickBytez.SKRAPE.Core.Extensions;
using ClickBytez.SKRAPE.Core.Providers;
using ClickBytez.SKRAPE.Core.Scraping;
using ClickBytez.Tools.Extensions.Enumerable;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Net.Http;

namespace ClickBytez.SKRAPE.Engine.Modules
{
    public static class SkrapeEngineModule
    {
        static readonly IConfiguration AppConfig = new ConfigurationBuilder().AddJsonFile("skrapesettings.json").Build();
        static readonly IScrapersProvider ScrapersProvider = new ScrapersProvider(AppConfig);
        
        public class ConfigurationModule : Autofac.Module
        {
            public ConfigurationModule() { }

            protected override void Load(ContainerBuilder builder)
            {
             
                builder.Register(x => AppConfig)
                    .InstancePerDependency()
                    .AsImplementedInterfaces();
               
                base.Load(builder);
            }
        }

        public class ProvidersModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                ILogger logger = Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

                builder.RegisterLogger(logger);
                builder.Register(x => ScrapersProvider)
                    .AsImplementedInterfaces()
                    .SingleInstance();

                builder.RegisterType<NotificationBus>()
                    .AsImplementedInterfaces()
                    .InstancePerDependency();

                base.Load(builder);
            }
        }

        public class FactoriesModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                string path = AppConfig.GetSkrapeEngineConfig().ScrapersAbsolutePath;
               
                ScrapersProvider.Scrapers.ForEach((scraper) => 
                {
                    builder.RegisterType(scraper).InstancePerDependency().AsSelf();
                });

                builder.Register<Func<Type, IScraper>>(context =>
                {
                    IComponentContext ctx = context.Resolve<IComponentContext>();
                    return (scraperType) =>
                    {
                        IScraper scraper =  (IScraper) ctx.Resolve(scraperType);
                        scraper.UseContext(ctx);
                        return scraper;
                    };
                });

                builder.RegisterType<ScrapersBus>()
                    .AsImplementedInterfaces()
                    .InstancePerDependency();

                builder.RegisterType<HttpClient>()
                   .AsSelf()
                   .InstancePerDependency();

                builder.RegisterType<HtmlDocument>()
                  .AsSelf()
                  .InstancePerDependency();
            }
        }
    }
}
