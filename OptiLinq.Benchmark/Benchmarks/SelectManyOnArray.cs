using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class SelectManyOnArray
{
	private int[][] array;
	private const int Count = 1000;

	public SelectManyOnArray()
	{
		array = Enumerable.Range(0, Count)
			.Select(x => Enumerable.Range(0, x).ToArray())
			.ToArray();
	}

	[Benchmark(Baseline = true)]
	public int LINQ()
	{
		var sum = 0;
		foreach (var i in array.SelectMany(x => x))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLINQWithDelegate()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().SelectMany<ArrayQuery<int>, int>(s => s.AsOptiQuery()))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLINQWithFunction()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().SelectMany<SelectManyFunction, ArrayQuery<int>, int>())
		{
			sum += i;
		}

		return sum;
	}
}