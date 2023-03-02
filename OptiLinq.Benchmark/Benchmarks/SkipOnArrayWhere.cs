using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class SkipOnArrayWhere
{
	private const int Count = 10000;
	public int[] array;

	public SkipOnArrayWhere()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Linq()
	{
		var sum = 0;
		foreach (var i in array.Where(x => true).Skip(1000))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;
		foreach (var i in array.AsOptiQuery().Where(x => true).Skip(1000))
		{
			sum += i;
		}

		return sum;
	}
}