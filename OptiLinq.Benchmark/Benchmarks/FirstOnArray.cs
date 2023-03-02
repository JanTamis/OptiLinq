using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class FirstOnArray
{
	private const int Count = 1000;
	private readonly int[] array;
	private readonly IEnumerable<int> enumerable;

	public FirstOnArray()
	{
		array = Enumerable.ToArray(Enumerable.Range(0, Count));
		enumerable = Enumerable.ToArray(Enumerable.Range(0, Count));
	}

	[Benchmark(Baseline = true)]
	public int Linq() => array.First();

	[Benchmark]
	public int EnumerableLinq() => enumerable.First();


	[Benchmark]
	public int OptiLinq() => array.AsOptiQuery().First();
}