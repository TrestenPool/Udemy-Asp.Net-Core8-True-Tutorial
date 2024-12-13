using System.Reflection;

namespace ReflectionDemo;

class Program{
  static void Main(string[] args){
    var assembly = Assembly.GetExecutingAssembly();

    while(true) {
      // read input from the console
      Console.WriteLine("Enter the substring of the type to look for: ");
      var line = Console.ReadLine();

      // break if no input
      if(string.IsNullOrWhiteSpace(line)) {
        break;
      }

      // gets all of the types where it is like what the user sent as input
      var types = assembly 
        .GetTypes()
        .Where(x => x.Name.Contains(line, StringComparison.OrdinalIgnoreCase))
        .ToArray();

    }

  }
}
