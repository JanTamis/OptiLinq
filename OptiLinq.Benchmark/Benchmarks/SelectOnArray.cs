using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class SelectOnArray
{
	private int[] array;
	private const int Count = 10000;

	public SelectOnArray()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public double Handmaded()
	{
		var sum = 0.0;

		foreach (var i in array)
		{
			var x = i * 2.0;
			sum += x;
		}

		return sum;
	}

	[Benchmark]
	public double LINQ()
	{
		var sum = 0.0;

		foreach (var i in array.Select(x => x * 2.0))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public double OptiLINQ()
	{
		var sum = 0.0;

		foreach (var i in array.AsOptiQuery().Select(x => x * 2.0))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public double OptiLINQWithFunction()
	{
		var sum = 0.0;

		foreach (var i in array.AsOptiQuery().Select<MultFunction, double>())
		{
			sum += i;
		}

		return sum;
	}
}