using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class SumOnSelectMany
{
	private int[][] array;
	private const int Count = 1000;

	public SumOnSelectMany()
	{
		array = Enumerable.Range(0, Count)
			.Select(x => Enumerable.Range(0, x).ToArray())
			.ToArray();
	}

	[Benchmark(Baseline = true)]
	public int LINQ()
	{
		return array.SelectMany(x => x).Sum();
	}

	[Benchmark]
	public int OptiLINQ()
	{
		return array.AsOptiQuery().SelectMany(x => x.AsOptiQuery()).Sum();
	}

	[Benchmark]
	public int OptiLINQWhereReturnIsOptiQuery()
	{
		return array.AsOptiQuery().SelectMany<ArrayQuery<int>, int>(x => x.AsOptiQuery()).Sum(x => x);
	}


	[Benchmark]
	public int OptiLinqWithFunction()
	{
		return array.AsOptiQuery().SelectMany<SelectManyFunction, ArrayQuery<int>, int>().Sum();
	}

	[Benchmark]
	public int OptiLINQWithFunctionWithForeach()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().SelectMany<SelectManyFunction, ArrayQuery<int>, int>())
		{
			sum += i;
		}

		return sum;
	}
}