using AoC.Core.Engine;
using System;
using System.Threading.Tasks;

namespace AoC.Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var engine = new AocEngine();

            Console.ReadKey();
        }
    }
}
