using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ArraySelectCount
{
	private const int Count = 10000;
	private readonly int[] array;

	public ArraySelectCount()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Linq()
	{
		return array
			.Select(x => x * 2)
			.Count();
	}

	[Benchmark]
	public int DelegateSelect()
	{
		return array.AsOptiQuery()
			.Select(x => x * 2)
			.Count();
	}

	[Benchmark]
	public int IFunctionSelect()
	{
		return array.AsOptiQuery()
			.Select<SelectFunction>()
			.Count();
	}
}