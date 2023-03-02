using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ArrayWhereSelectSum
{
	private const int Count = 10000;
	private readonly int[] array;

	public ArrayWhereSelectSum()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int HandmadedCode()
	{
		int sum = 0;
		for (int i = 0; i < array.Length; i++)
		{
			var elt = array[i];
			if ((elt & 1) == 0)
			{
				var mult = elt * 2;
				sum += mult;
			}
		}

		return sum;
	}

	[Benchmark]
	public int SysLinq() => array
		.Where(x => (x & 1) == 0)
		.Select(x => x * 2)
		.Sum();

	[Benchmark]
	public int OptiRangeWhereSelectSumWithDelegate() => array
		.AsOptiQuery()
		.Where(x => (x & 1) == 0)
		.Select(x => x * 2)
		.Sum(x => x);

	[Benchmark]
	public int OptiRangeWhereSelectSum()
	{
		return array
			.AsOptiQuery()
			.Where<IsEven<int>>()
			.Select<SelectFunction, int>()
			.Sum();
	}
}