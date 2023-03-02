using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class LastOnArray
{
	private const int Count = 1000;
	private readonly int[] array;
	private readonly IEnumerable<int> enumerable;

	public LastOnArray()
	{
		array = Enumerable.ToArray(Enumerable.Range(0, Count));
		enumerable = Enumerable.ToArray(Enumerable.Range(0, Count));
	}

	[Benchmark(Baseline = true)]
	public int Linq() => array.Last();

	[Benchmark]
	public int EnumerableLinq() => enumerable.Last();


	[Benchmark]
	public int OptiLinq() => array.AsOptiQuery().Last();
}