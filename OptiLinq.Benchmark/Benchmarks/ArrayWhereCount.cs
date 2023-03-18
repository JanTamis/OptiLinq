using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ArrayWhereCount
{
	private const int Count = 10000;
	private readonly int[] array;

	public ArrayWhereCount()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int HandmadedCode()
	{
		var count = 0;

		for (var i = 0; i < array.Length; i++)
		{
			var elt = array[i];

			if ((elt & 1) == 0)
			{
				count++;
			}
		}

		return count;
	} 

	[Benchmark]
	public int SysLinq()
	{
		return array
			.Where(x => (x & 1) == 0)
			.Count();
	}

	[Benchmark]
	public int DelegateWhere()
	{
		return array.AsOptiQuery()
			.Where(x => (x & 1) == 0)
			.Count();
	}

	[Benchmark]
	public int SysLinqWithoutWhere()
	{
		return array
			.Count(x => (x & 1) == 0);
	}

	[Benchmark]
	public int DelegateWithoutWhere()
	{
		return array
			.AsOptiQuery()
			.Count(x => (x & 1) == 0);
	}

	[Benchmark]
	public int IFunctionWhere()
	{
		return array
			.AsOptiQuery()
			.Where<IsEven<int>>()
			.Count();
	}

	[Benchmark]
	public int OptiLinqOptimized()
	{
		return array
			.AsOptiQuery()
			.Count<IsEven<int>, int>();
	}

	[Benchmark]
	public int OptiLinqIFunctionOptimized()
	{
		return array
			.AsOptiQuery()
			.Count<int, int>(x => (x & 1) == 0);
	}
}