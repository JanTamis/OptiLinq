using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ElementAtOnArray
{
	private const int Count = 1000;
	private readonly int[] array;
	private readonly IEnumerable<int> enumerable;

	public ElementAtOnArray()
	{
		array = Enumerable.ToArray(Enumerable.Range(0, Count));
		enumerable = Enumerable.ToArray(Enumerable.Range(0, Count));
	}

	[Benchmark(Baseline = true)]
	public int Linq() => array.ElementAt(Count / 2);

	[Benchmark]
	public int EnumerableLinq() => enumerable.ElementAt(Count / 2);


	[Benchmark]
	public int OptiLinq() => array.AsOptiQuery().ElementAt(Count / 2);
}