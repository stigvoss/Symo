using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Library.Modules
{
    public static class ModuleLoader
    {
        public static IEnumerable<Type> LoadModules()
        {
            List<Type> types = new List<Type>();

            var fileNames = Directory.EnumerateFiles("Modules", "*.dll");
            foreach(var fileName in fileNames)
            {
                var fileInfo = new FileInfo(fileName);
                var assembly = Assembly.LoadFile(fileInfo.FullName);
                types.AddRange(assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IMonitor))));
            }

            return types;
        }
    }
}
