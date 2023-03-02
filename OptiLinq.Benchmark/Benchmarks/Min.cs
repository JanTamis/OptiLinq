using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Min
{
	private const int Count = 10000;

	[Benchmark(Baseline = true)]
	public int Handmaded()
	{
		var min = int.MaxValue;

		for (var index = 0; index < Count; index++)
		{
			if (index < min)
				min = index;
		}

		return min;
	}

	[Benchmark]
	public int LINQ()
	{
		return Enumerable.Range(0, Count).Min();
	}

	[Benchmark]
	public int StructLINQ()
	{
		return OptiQuery.Range(0, Count).Min();
	}


	[Benchmark]
	public int ConvertMin()
	{
		return Enumerable.Range(0, Count).AsOptiQuery().Min();
	}
}