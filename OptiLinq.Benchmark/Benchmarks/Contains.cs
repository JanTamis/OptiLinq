using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Contains
{
	private const int Count = 10_000;
	private readonly int[] array;

	public Contains()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public bool Array()
	{
		return array.Contains(5_000);
	}

	[Benchmark]
	public bool Linq()
	{
		return array.AsEnumerable().Contains(5000);
	}

	[Benchmark]
	public bool OptiLinq()
	{
		return array.AsOptiQuery().Contains(5000);
	}
}