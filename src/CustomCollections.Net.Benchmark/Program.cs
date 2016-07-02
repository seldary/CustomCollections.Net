using BenchmarkDotNet.Running;

namespace CustomCollections.Net.Benchmark
{
    public class Program
    {

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ReadonlyDictionaryBenchmark>();
        }
    }
}
