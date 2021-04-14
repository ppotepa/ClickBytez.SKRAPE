using Microsoft.Extensions.Configuration;
using System;
using System.Threading;

namespace ClickBytez.SKRAPE.Engine
{
    public sealed class SkrapeEngine : ISkrapeEngine
    {
        private readonly ScrapeEngineConfiguration Config;
        private int Ticks = 0;

        public bool Initialized { get; private set; }
        public bool IsRunning { get; internal set; }
        public SkrapeEngine(IConfiguration configuration)
        {
            Config = configuration.ScrapeEngineConfig();
        }

        public void Initialize() 
        {
            this.Initialized = true;
        }

        public void Start() 
        {
            if (Initialized is false)
                throw new SkrapeEngineNotInitializedException("SKRAPE was not initialized.");

            ThreadPool.QueueUserWorkItem(this.MainThread);
        }

        private void MainThread(Object stateInfo) 
        {
            while (IsRunning)
            {
                Thread.Sleep(100);
                Console.WriteLine($"Ticks : {Ticks++}");
            }
        }
    }
}