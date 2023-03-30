using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ImmutableArrayWhereSelectSum
{
	private const int Count = 10000;
	private readonly ImmutableArray<int> array;

	public ImmutableArrayWhereSelectSum()
	{
		array = Enumerable.Range(0, Count).ToImmutableArray();
	}

	[Benchmark(Baseline = true)]
	public int LINQ()
	{
		return array
			.Where(x => (x & 1) == 0)
			.Select(x => x * 2)
			.Sum();
	}

	[Benchmark]
	public int OptiLinq()
	{
		return array
			.AsOptiQuery()
			.Where(x => (x & 1) == 0)
			.Select(x => x * 2)
			.Sum();
	}

	[Benchmark]
	public int OptiLinqFunction()
	{
		return array
			.AsOptiQuery()
			.Where<IsEven<int>>()
			.Select<SelectFunction>()
			.Sum();
	}
}