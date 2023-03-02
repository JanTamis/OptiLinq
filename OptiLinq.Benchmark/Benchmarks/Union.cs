using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Union
{
	private const int Count = 10000;
	private int[] array1;
	private int[] array2;

	public Union()
	{
		var size1 = Count / 2;
		var size2 = Count - size1;
		array1 = Enumerable.Range(0, size1).ToArray();
		array2 = Enumerable.Range(size1 - 100, size2).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Linq()
	{
		var sum = 0;
		foreach (var i in array1.Union(array2))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;

		foreach (var i in array1.AsOptiQuery().Union(array2.AsOptiQuery()))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqSum()
	{
		return array1.AsOptiQuery().Union(array2.AsOptiQuery()).Sum();
	}

	[Benchmark]
	public int OptiLinqComparer()
	{
		var sum = 0;
		var comparer = new DefaultStructEqualityComparer();

		foreach (var i in array1.AsOptiQuery().Union(array2.AsOptiQuery(), comparer))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqComparerSum()
	{
		var comparer = new DefaultStructEqualityComparer();

		return array1.AsOptiQuery()
			.Union(array2.AsOptiQuery(), comparer)
			.Sum();
	}
}