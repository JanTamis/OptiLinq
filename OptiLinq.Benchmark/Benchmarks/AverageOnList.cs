using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class AverageOnList
{
	private readonly List<int> list;

	public AverageOnList()
	{
		list = Enumerable.Range(-1, 10000).ToList();
	}

	[Benchmark(Baseline = true)]
	public double Linq()
	{
		return list.Average();
	}

	[Benchmark]
	public double OptiLinq()
	{
		return list.AsOptiQuery().Average();
	}
}