using System.Reflection;

namespace ReflectionDemo;

class Program{
  static void Main(string[] args){
    // get the assembly that is currently running
    var assembly = Assembly.GetExecutingAssembly();
    var allTypes = assembly.GetTypes();

    while(true) {
      // read input from the console
      Console.WriteLine("Enter the substring of the type to look for: ");
      var line = Console.ReadLine();

      // break if no input
      if(string.IsNullOrWhiteSpace(line)) {
        break;
      }

      // gets all of the types where it is like what the user sent as input
      var types = allTypes
        // type name contains the string we typed in
        .Where(x => x.Name.Contains(line, StringComparison.OrdinalIgnoreCase))
        
        // convert to array
        .ToArray();
    }

  }
}
