using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ToArrayOnArrayWhere
{
	private const int Count = 10000;
	private readonly int[] array;

	public ToArrayOnArrayWhere()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int[] Linq()
	{
		return array
			.Where(x => (x & 1) == 0)
			.ToArray();
	}


	[Benchmark]
	public int[] OptiLinq()
	{
		return array
			.AsOptiQuery()
			.Where(x => (x & 1) == 0)
			.ToArray();
	}


	[Benchmark]
	public int[] OptiLinqWithFunction()
	{
		return array
			.AsOptiQuery()
			.Where<IsEven<int>>()
			.ToArray();
	}
}