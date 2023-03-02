using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ForEachOnListOfString
{
	private readonly List<string> list;

	public ForEachOnListOfString()
	{
		list = Enumerable.Range(-1, 10000).Select(x => x.ToString()).ToList();
	}

	[Benchmark(Baseline = true)]
	public int Default()
	{
		var sum = 0;
		foreach (var i in list)
		{
			sum += i.Length;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;
		foreach (var i in list.AsOptiQuery())
		{
			sum += i.Length;
		}

		return sum;
	}
}