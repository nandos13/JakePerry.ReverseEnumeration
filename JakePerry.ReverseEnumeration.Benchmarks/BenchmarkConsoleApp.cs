using BenchmarkDotNet.Running;

namespace JakePerry.ReverseEnumeration.Benchmarks
{
    public class BenchmarkConsoleApp
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ListBenchmarks>();
            BenchmarkRunner.Run<ArraySegmentBenchmarks>();
        }
    }
}
