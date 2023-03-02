using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class SumOnList
{
	private readonly List<int> list;

	public SumOnList()
	{
		list = Enumerable.Range(-1, 10000).ToList();
	}

	[Benchmark(Baseline = true)]
	public int Linq()
	{
		return list.Sum();
	}

	[Benchmark]
	public int OptiLinq()
	{
		return list.AsOptiQuery().Sum();
	}

	[Benchmark]
	public int OptiLinqOptimized()
	{
		return list.AsOptiQuery().Sum<int, ListQuery<int>>();
	}
}