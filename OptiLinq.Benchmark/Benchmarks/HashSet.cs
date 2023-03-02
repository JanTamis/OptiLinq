using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class HashSet
{
	private HashSet<int> hashset;

	[Params(2, 100, 1000)]
	public int ItemCount { get; set; }

	[GlobalSetup]
	public void GlobalSetup()
	{
		hashset = Enumerable
			.Range(0, ItemCount)
			.ToHashSet();
	}

	[Benchmark(Baseline = true)]
	public int LINQ()
	{
		var sum = 0;
		foreach (var i in hashset)
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int LinqSum()
	{
		return hashset.Sum();
	}

	[Benchmark]
	public int OptiLINQ()
	{
		var sum = 0;
		foreach (var i in hashset.AsOptiQuery())
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLINQSum()
	{
		return hashset.AsOptiQuery().Sum();
	}
}