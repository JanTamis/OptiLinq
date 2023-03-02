using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ToArrayOnArraySelect
{
	private const int Count = 10000;
	private readonly int[] array;

	public ToArrayOnArraySelect()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int[] Linq()
	{
		return array
			.Select(x => x * 2)
			.ToArray();
	}


	[Benchmark]
	public int[] OptiLinq()
	{
		return array
			.AsOptiQuery()
			.Select(x => x * 2)
			.ToArray();
	}

	[Benchmark]
	public int[] OptiLinqWithFunction()
	{
		return array
			.AsOptiQuery()
			.Select<SelectFunction, int>()
			.ToArray();
	}
}