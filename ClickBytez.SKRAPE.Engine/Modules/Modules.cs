using Autofac;
using AutofacSerilogIntegration;
using ClickBytez.SKRAPE.Core.Extensions;
using ClickBytez.SKRAPE.Core.Scraping;
using ClickBytez.Tools.Enumerable;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System;

namespace ClickBytez.SKRAPE.Engine.Modules
{
    public static class Modules
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
                var logger = Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console()
                .CreateLogger();

                builder.RegisterLogger(logger);

                builder.Register(x => ScrapersProvider)
                    .AsImplementedInterfaces()
                    .SingleInstance();

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
                    var ctx = context.Resolve<IComponentContext>();
                    return scraperType =>
                    {
                        return  (IScraper)ctx.Resolve(scraperType);
                    };
                });
            }
        }
    }
}
