using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class OrderByArrayOfInt
{
	private const int Count = 10_000;
	private int[] array;

	public OrderByArrayOfInt()
	{
		array = new int[Count];

		OptiQuery.Random(42)
			.CopyTo(array);
	}

	[Benchmark(Baseline = true)]
	public int LINQ()
	{
		var sum = 0;

		foreach (var i in array.OrderBy(Int32.IsOddInteger).ThenBy(x => x))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int LINQSum()
	{
		return array
			.OrderBy(Int32.IsOddInteger)
			.ThenBy(x => x)
			.Sum();
	}

	[Benchmark]
	public int OptiLinqOrder()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().OrderBy<bool, IsOdd<int>>().ThenBy<int, Identity<int>>())
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqOrderSum()
	{
		return array
			.AsOptiQuery()
			.OrderBy<bool, IsOdd<int>>()
			.ThenBy<int, Identity<int>>()
			.Sum();
	}

	[Benchmark]
	public int OptiLinqOrderFunc()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().OrderBy(Int32.IsOddInteger).ThenBy(x => x))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqOrderFuncSum()
	{
		return array
			.AsOptiQuery()
			.OrderBy(Int32.IsOddInteger)
			.ThenBy(x => x)
			.Sum();
	}
}