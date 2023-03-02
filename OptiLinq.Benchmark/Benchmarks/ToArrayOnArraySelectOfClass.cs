using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ToArrayOnArraySelectOfClass
{
	private const int Count = 10000;
	private readonly Container[] array;

	public ToArrayOnArraySelectOfClass()
	{
		array = Enumerable.Range(0, Count).Select(x => new Container(x)).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int[] Linq()
	{
		return array
			.Select(x => x.Element)
			.ToArray();
	}


	[Benchmark]
	public int[] OptiLinq()
	{
		return array
			.AsOptiQuery()
			.Select(x => x.Element)
			.ToArray();
	}

	[Benchmark]
	public int[] OptiLinqWithFunction()
	{
		return array
			.AsOptiQuery()
			.Select<ContainerSelect, int>()
			.ToArray();
	}
}