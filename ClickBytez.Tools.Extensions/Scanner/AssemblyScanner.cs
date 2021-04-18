using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ClickBytez.Tools.Assemblies
{
    public class AssemblyScanner<TTargetType>
    {
        private const string DLL = "*.dll";
        private readonly string Path;
        private readonly Type TargetType;


        public AssemblyScanner(string path)
        {
            this.Path = path;
            this.TargetType = typeof(TTargetType);
        }

        public Assembly[] Assemblies
        {
            get
            {
                if (this.assemblies is null)
                    assemblies = this.Files.Select(LoadAssemblyFomPath).ToArray();
                return assemblies;
            }

            set
            {
                assemblies = value;
            }
        }

        public string[] Files
        {
            get
            {
                if (files is null)
                    files = Directory.GetFiles(Path, DLL);
                return files;
            }
            set
            {
                files = value;
            }
        }
        public Type[] FoundTypes
        {
            get
            {
                if (this.foundTypes is null)
                    foundTypes = Assemblies.SelectMany(AssemblyImplementsTargetType).ToArray();
                return foundTypes;
            }

            set
            {
                foundTypes = value;
            }
        }

        IEnumerable<Type> AssemblyImplementsTargetType(Assembly assembly) => assembly.GetTypes().Where(TypeIsValid);
        Assembly LoadAssemblyFomPath(string filePath) => Assembly.Load(File.ReadAllBytes(filePath));

        bool TypeIsValid(Type type)
        {
            bool isSubClassOf = type.IsSubclassOf(this.TargetType);
            bool implementsInterface = type.GetInterfaces().Contains(this.TargetType);
            bool ImplementsFromTargetType = isSubClassOf || implementsInterface;

            return ImplementsFromTargetType;
        }

        #region BACKING_FIELDS
        private Assembly[] assemblies = null;
        private string[] files = null;
        private Type[] foundTypes = null;
        #endregion
    }
}
