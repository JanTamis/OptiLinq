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
		var rand = new Random(42);
		var list = new List<int>();
		for (int i = 0; i < Count; i++)
		{
			list.Add(rand.Next());
		}

		array = list.ToArray();
	}

	[Benchmark(Baseline = true)]
	public int LINQ()
	{
		var sum = 0;
		foreach (var i in array.OrderBy(x => x))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqOrder()
	{
		var sum = 0;
		foreach (var i in array.AsOptiQuery().Order())
		{
			sum += i;
		}

		return sum;
	}


	[Benchmark]
	public int OptiLinqOrderComparer()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().Order(new IntComparer()))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqOrderSum()
	{
		return array.AsOptiQuery().Order().Sum();
	}
}