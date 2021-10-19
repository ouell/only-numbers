using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace OnlyNumbers
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<OnlyNumberTests>();
        }
    }
}