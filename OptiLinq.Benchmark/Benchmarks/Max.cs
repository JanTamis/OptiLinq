using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Max
{
	private const int Count = 10000;

	[Benchmark(Baseline = true)]
	public int Handmaded()
	{
		var max = int.MinValue;

		for (var index = 0; index < Count; index++)
		{
			if (index > max)
				max = index;
		}

		return max;
	}

	[Benchmark]
	public int LINQ()
	{
		return Enumerable.Range(0, Count).Max();
	}

	[Benchmark]
	public int StructLINQ()
	{
		return OptiQuery.Range(0, Count).Max();
	}


	[Benchmark]
	public int ConvertMin()
	{
		return Enumerable.Range(0, Count).AsOptiQuery().Max();
	}
}