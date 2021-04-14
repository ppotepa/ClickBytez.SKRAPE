using Autofac;
using ClickBytez.SKRAPE.Engine;
using Microsoft.Extensions.Configuration;

namespace ClickBytez.SKRAPE.Demo
{
    class Program
    {
        static IConfiguration SKRAPEConfig(IComponentContext ctx) => new ConfigurationBuilder().AddJsonFile("skrapesettings.json").Build();
        static IContainer Root => GetCompositionRoot();
        static ContainerBuilder builder = new ContainerBuilder();

        private static IContainer GetCompositionRoot()
        {
            builder.Register(SKRAPEConfig);
            
            builder.RegisterType<SkrapeEngine>();

            return builder.Build();
        }
        static void Main(string[] args)
        {
            Root.Resolve<SkrapeEngine>().Start();
        }
    }
}
