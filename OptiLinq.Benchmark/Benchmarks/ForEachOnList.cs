using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ForEachOnList
{
	private readonly List<int> list;

	public ForEachOnList()
	{
		list = Enumerable.Range(-1, 10000).ToList();
	}

	[Benchmark(Baseline = true)]
	public int Default()
	{
		var sum = 0;
		foreach (var i in list)
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;
		foreach (var i in list.AsOptiQuery())
		{
			sum += i;
		}

		return sum;
	}
}