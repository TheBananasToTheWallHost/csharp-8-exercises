using System;
using System.Linq;
using System.Reflection;

namespace Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var r in Assembly.GetEntryAssembly().GetReferencedAssemblies()){
                var a = Assembly.Load(new AssemblyName(r.FullName));
                int methodCount = 0;
                foreach (var t in a.DefinedTypes)
                {
                    methodCount += t.GetMethods().Count();
                }

                Console.WriteLine(
                    "{0:N0} types with {1:N0} methods in {2} assembly.", 
                    a.DefinedTypes.Count(),
                    methodCount,
                    r.Name);
            }
        }
    }
}
