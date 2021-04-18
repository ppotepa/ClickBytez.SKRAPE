using Autofac;
using ClickBytez.SKRAPE.Engine;
using ClickBytez.SKRAPE.Engine.Extensions;
using System.Threading;

namespace ClickBytez.SKRAPE.Demo
{
    class Program
    {
        private static readonly ContainerBuilder Builder = new ContainerBuilder();
        private static readonly IContainer Root = GetCompositionRoot();

        private static IContainer GetCompositionRoot()
        {
            Builder.RegisterSkrapeEngine();
            return Builder.Build();
        }

        static void Main(string[] args)
        {
            SkrapeEngine Engine = Root.Resolve<SkrapeEngine>().Initialize().Start();

            while (Engine.IsRunning)
            {
                Thread.Sleep(100);
            }
        }
    }
}
