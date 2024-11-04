using Delegates_and_events_example;
using System.Security.Cryptography.X509Certificates;
using static System.Console;
using System; 

namespace Delegates_and_events_example {
    class Program {
        public static void Main() {
            Dictionary<int, string> dict = new Dictionary<int, string> {
                {1, "Josepth"},
                {2, "David"},
                {3, "Kyle"},
            };

            foreach(var student in dict) {
                Console.WriteLine($"{student.Key} {student.Value}");
            }

            WriteLine($"But {dict[3]} is the best");
        }
    }
}


