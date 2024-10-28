namespace ExtensionMethodDemo {
    internal class Program {
        static void Main(string[] args) {
            // create the object
            Product product = new Product() { ProductCost = 125, DiscountPercentage = 10 };


            // call the extension method
            Console.WriteLine(product.GetDiscount());
        }
    }

    // target class
    class Product {
        // properties
        public int ProductCost { get; set; }
        public double DiscountPercentage { get; set; }
    }

    // static class extension methods
    static class ProductExtensions {
        public static double GetDiscount(this Product product) {        // must use the this keyword to reference the class that you want to attach the method to
            return (product.ProductCost * product.DiscountPercentage) / 100;
        }
    }
}
