using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Concat
{
	private const int Count = 5_000;

	private readonly int[] array1;
	private readonly int[] array2;

	public Concat()
	{
		array1 = Enumerable.Range(0, Count).ToArray();
		array2 = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark]
	public int Linq()
	{
		var sum = 0;
		foreach (var i in array1.Concat(array2))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int LinqSum()
	{
		return array1.Concat(array2).Sum();
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;

		foreach (var i in array1.AsOptiQuery().Concat(array2.AsOptiQuery()))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqSum()
	{
		return array1.AsOptiQuery().Concat(array2.AsOptiQuery()).Sum();
	}
}