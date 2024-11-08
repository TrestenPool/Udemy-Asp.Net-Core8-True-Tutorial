using System.Numerics;

namespace GenericsExample {
    internal class Program {
        static void Main(string[] args) {

            var x = new GFG<string>("Hello world");
            x.something();
        }
    }

    class GFG<T> {
        T MyVariable {get; set; }

        public GFG(T myVariable) {
            MyVariable = myVariable;
        }

        public void something() {
            Console.WriteLine($"Variable is of type {MyVariable.GetType()}");
            Console.WriteLine($"MyVariable = {MyVariable}");
        }
    }

}
