using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Sum
{
	private const int Count = 10000;

	[Benchmark(Baseline = true)]
	public int ForSum()
	{
		var sum = 0;

		for (var i = 0; i < Count; i++)
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int SysSum()
	{
		return Enumerable.Range(0, Count).Sum();
	}

	[Benchmark]
	public int OptiSum()
	{
		return OptiQuery.Range(0, Count).Sum();
	}

	[Benchmark]
	public int OptiForEachSum()
	{
		var sum = 0;

		foreach (var i in OptiQuery.Range(0, Count))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int ConvertSum()
	{
		return Enumerable.Range(0, Count).AsOptiQuery().Sum();
	}
}