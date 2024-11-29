using System.Reflection;

namespace Sample;

class Program
{
    static void Main(string[] args){
      Assembly? myapp = Assembly.GetEntryAssembly();

      if(myapp == null){
        Console.WriteLine("myapp is null");
        return;
      }

      // loop through the assemblies that my app references
      foreach(AssemblyName name in myapp.GetReferencedAssemblies()){

        // load assembly so we can read the results
        Assembly a = Assembly.Load(name);

        int methodCount = 0;

        foreach(TypeInfo t in a.DefinedTypes){
          methodCount += t.GetMethods().Count();
        }

        Console.WriteLine($"{a.DefinedTypes.Count()} types with {methodCount} methods in assembly {name.Name}");
      }
    }
}
