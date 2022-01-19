using BenchmarkDotNet.Running;

namespace JakePerry.ReverseEnumerable.Benchmarks
{
    public class BenchmarkConsoleApp
    {
        static void Main(string[] args)
        {
            var results = BenchmarkRunner.Run<ReverseEnumerableBenchmarks>();
        }
    }
}
