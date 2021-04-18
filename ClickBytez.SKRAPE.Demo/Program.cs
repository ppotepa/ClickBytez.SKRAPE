using Autofac;
using ClickBytez.SKRAPE.Core;
using ClickBytez.SKRAPE.Engine;
using ClickBytez.SKRAPE.Engine.Modules;
using System.Threading;

namespace ClickBytez.SKRAPE.Demo
{
    class Program
    {
        private static readonly ContainerBuilder Builder = new ContainerBuilder();
        private static readonly IContainer Root = GetCompositionRoot();

        private static IContainer GetCompositionRoot()
        {
            ScrapeEngineConfiguration config = new ScrapeEngineConfiguration();

            Builder.RegisterModule<Modules.ConfigurationModule>();
            Builder.RegisterModule<Modules.ProvidersModule>();
            Builder.RegisterModule<Modules.FactoriesModule>();
            Builder.RegisterType<SkrapeEngine>().SingleInstance();
            
            return Builder.Build();
        }

        static void Main(string[] args)
        {
            SkrapeEngine Engine = Root.Resolve<SkrapeEngine>().Initialize();
            bool started = Engine.Start();
          
            while (Engine.IsRunning)
            {
                Thread.Sleep(100);
            }
        }
    }
}
