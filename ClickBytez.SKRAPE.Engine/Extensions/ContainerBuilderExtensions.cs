using Autofac;
using static ClickBytez.SKRAPE.Engine.Modules.SkrapeEngineModule;

namespace ClickBytez.SKRAPE.Engine.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterSkrapeEngine(this ContainerBuilder @this)
        {
            @this.RegisterModule<ConfigurationModule>();
            @this.RegisterModule<ProvidersModule>();
            @this.RegisterModule<FactoriesModule>();
            @this.RegisterType<SkrapeEngine>().SingleInstance();
        }
    }
}
