using ClickBytez.SKRAPE.Core;
using ClickBytez.SKRAPE.Core.Bus;
using ClickBytez.SKRAPE.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Threading;

namespace ClickBytez.SKRAPE.Engine
{
    public sealed class SkrapeEngine : ISkrapeEngine
    {
        private readonly IScrapersBus Bus;
        private readonly ScrapeEngineConfiguration Config;       
        private readonly ILogger Logger;

        public SkrapeEngine(IConfiguration configuration, IScrapersBus bus, ILogger logger)
        {
            this.Config = configuration.GetSkrapeEngineConfig();
            this.Bus = bus;
            this.Logger = logger;
        }

        public bool Initialized { get; private set; }
        public bool IsRunning { get; internal set; }

        public SkrapeEngine Initialize() 
        {
            Initialized = true;
            return this;
        }

        public SkrapeEngine Start() 
        {
            if (Initialized is false)
                throw new SkrapeEngineNotInitializedException("SKRAPE was not initialized.");
            
            ThreadPool.QueueUserWorkItem(this.MainThread);
            
            while (IsRunning is false)
            {
                Thread.Sleep(100);
            }

            return this;
        }

        private void MainThread(object stateInfo) 
        {
            IsRunning = true;

            while (IsRunning)
            {
                this.Bus.Run();
                Thread.Sleep(50);
                Console.Clear();
            }           
        }
    }
}