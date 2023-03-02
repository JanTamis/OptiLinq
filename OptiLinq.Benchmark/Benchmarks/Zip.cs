using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Zip
{
	private const int Count1 = 10000;
	private const int Count2 = 5000;
	public int[] array1;
	public int[] array2;

	public Zip()
	{
		array1 = Enumerable.Range(0, Count1).ToArray();
		array2 = Enumerable.Range(0, Count2).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Linq()
	{
		var sum = 0;

		foreach (var i in array1.Zip(array2))
		{
			sum += i.First + i.Second;
		}

		return sum;
	}

	[Benchmark]
	public int LinqFunction()
	{
		var sum = 0;

		foreach (var i in array1.Zip(array2, (x, y) => x + y))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int LinqFunctionSum()
	{
		return array1.Zip(array2, (x, y) => x + y).Sum();
	}

	[Benchmark]
	public int LinqSum()
	{
		return array1.Zip(array2).Sum(x => x.First + x.Second);
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;

		foreach (var i in array1.AsOptiQuery().Zip(array2.AsOptiQuery(), (x, y) => x + y))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqSum()
	{
		return array1.AsOptiQuery().Zip(array2.AsOptiQuery(), (x, y) => x + y).Sum();
	}

	[Benchmark]
	public int OptiLinqFunction()
	{
		var sum = 0;

		foreach (var i in array1.AsOptiQuery().Zip<int, Add, ArrayQuery<int>>(array2.AsOptiQuery()))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqFunctionSum()
	{
		return array1.AsOptiQuery().Zip<int, Add, ArrayQuery<int>>(array2.AsOptiQuery()).Sum();
	}

	private struct Add : IFunction<int, int, int>
	{
		public int Eval(in int x, in int y) => x + y;
	}
}