using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class IList
{
	private const int Count = 10_000;
	private readonly IList<int> list;

	public IList()
	{
		list = Enumerable.Range(0, Count).ToList();
	}

	[Benchmark(Baseline = true)]
	public int Linq()
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