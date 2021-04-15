using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ClickBytez.Tools.Scanners
{
    public class AssemblyScanner
    {
        private readonly string path;
        private string PathConcat => Path.Combine(Directory.GetCurrentDirectory(), path);

        public AssemblyScanner(string path)
        {
            this.path = path;
        }

        public string[] Files => Directory.GetFiles(this.path, "*.dll");
        public Assembly[] Scan() => Files.Select(file => Assembly.LoadFile(file)).ToArray();

        public Assembly[] ScanWithSubclass<TTargetType>() => Scan().Where(assembly => assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(TTargetType)))
                                                                .Count() != 0)
                                                                .ToArray();

        public Type[] ScanForTypes<TTargetType>() => Scan()
            .SelectMany(assembly => assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(TTargetType))))
            .ToArray();
        public IEnumerable<byte[]> GetRaw() => Files.Select(file => File.ReadAllBytes(file)).ToArray();

    }
}
