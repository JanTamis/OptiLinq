using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Average
{
	private const int Count = 10000;

	[Benchmark(Baseline = true)]
	public double ForAverage()
	{
		var sum = 0;

		for (var i = 0; i < Count; i++)
		{
			sum += i;
		}

		return sum / (double)Count;
	}

	[Benchmark]
	public double SysAverage()
	{
		return Enumerable.Range(0, Count).Average();
	}

	[Benchmark]
	public double OptiAverage()
	{
		return OptiQuery.Range(0, Count).Average();
	}

	[Benchmark]
	public double OptiForEachAverage()
	{
		var sum = 0;
		var count = 0;

		foreach (var i in OptiQuery.Range(0, Count))
		{
			sum += i;
			count++;
		}

		return sum / (double)count;
	}

	[Benchmark]
	public double ConvertAverage()
	{
		return Enumerable.Range(0, Count).AsOptiQuery().Average();
	}
}