using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Repeat
{
	private const int Count = 10000;
	private const int value = 12;

	[Benchmark(Baseline = true)]
	public int EnumerableRepeat()
	{
		var sum = 0;

		foreach (var result in Enumerable.Repeat(value, Count))
		{
			sum += result;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqeRepeat()
	{
		var sum = 0;

		foreach (var result in OptiQuery.Repeat(value).Take(Count))
		{
			sum += result;
		}

		return sum;
	}
}