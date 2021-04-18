using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClickBytez.SKRAPE.Core.Providers
{
    public interface IScrapersProvider
    {
        IEnumerable<Assembly> Assemblies { get; }
        IEnumerable<byte[]> RawAssemblies { get; }
        IEnumerable<Type> Scrapers { get; }
    }
}