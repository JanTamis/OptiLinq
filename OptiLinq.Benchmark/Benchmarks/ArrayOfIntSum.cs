using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ArrayOfIntSum
{
	private readonly IEnumerable<int> sysArray;
	private readonly int Count = 10_000;
	private readonly int[] array;

	public ArrayOfIntSum()
	{
		sysArray = Enumerable.ToArray(Enumerable.Range(0, Count));
		array = Enumerable.ToArray(Enumerable.Range(0, Count));
	}

	[Benchmark]
	public int Handmaded()
	{
		var sum = 0;
		var enumerable = array;

		for (var i = 0; i < Count; i++)
		{
			sum += enumerable[i];
		}

		return sum;
	}

	[Benchmark]
	public int EnumerableLINQ() => sysArray.Sum();

	[Benchmark(Baseline = true)]
	public int ArrayLINQ() => array.Sum();

	[Benchmark]
	public int OptiLinq() => array.AsOptiQuery().Sum<int, ArrayQuery<int>>();
}