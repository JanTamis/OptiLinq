using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class WhereOnArray
{
	private int[] array;
	private const int Count = 10000;

	public WhereOnArray()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Handmaded()
	{
		var sum = 0;

		foreach (var i in array)
		{
			if (i % 2 == 0)
				sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int LINQ()
	{
		var sum = 0;

		foreach (var i in array.Where(x => x % 2 == 0))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int LinqSum()
	{
		return array.Where(x => x % 2 == 0).Sum();
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().Where(x => x % 2 == 0))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqSum()
	{
		return array.AsOptiQuery().Where(x => x % 2 == 0).Sum();
	}

	[Benchmark]
	public int OptiLinqWithFunction()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().Where<IsEven<int>>())
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqWithFunctionSum()
	{
		return array.AsOptiQuery().Where<IsEven<int>>().Sum();
	}
}