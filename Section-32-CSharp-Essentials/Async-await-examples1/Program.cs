using System.IO;
using System;
using System.Threading.Tasks;

namespace AsyncDemo;

class Program {
    static async Task Main(string[] args) {
        await SummonDogLocally();
    }

    static async Task SummonDogLocally() {
        Console.WriteLine("summoning dog locally ...");
        string contents = await File.ReadAllTextAsync(@"C:\Users\trestenp\OneDrive - City of Corpus Christi\Documents\Github\Udemy-Asp.Net-Core8-True-Tutorial\Section-32-CSharp-Essentials\Async-await-examples1\myfile.txt");
        Console.WriteLine($"contents = {contents}");
    }
}
