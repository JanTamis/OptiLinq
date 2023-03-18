using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class RangeToArray
{
	private const int Count = 10_000;

	[Benchmark(Baseline = true)]
	public int[] Linq()
	{
		return Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark]
	public int[] OptiLinq()
	{
		return OptiQuery.Range(0, Count).ToArray();
	}
}