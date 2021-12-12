using AoC.Core.Engine;
using System;
using System.Threading.Tasks;

namespace AoC.Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var engine = new AocEngine(Console.ReadLine, Console.WriteLine);
            await engine.Start();
        }
    }
}
