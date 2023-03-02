using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class UnionOnBigStruct
{
	private const int Count = 10000;
	private StructContainer[] array1;
	private StructContainer[] array2;

	public UnionOnBigStruct()
	{
		var size1 = Count / 2;
		var size2 = Count - size1;
		array1 = Enumerable.Range(0, size1).Select(StructContainer.Create).ToArray();
		array2 = Enumerable.Range(size1 - 100, size2).Select(StructContainer.Create).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Linq()
	{
		var sum = 0;
		foreach (var i in array1.Union(array2))
		{
			sum += i.Element;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;
		foreach (var i in array1.AsOptiQuery().Union(array2.AsOptiQuery()))
		{
			sum += i.Element;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqWithComparer()
	{
		var sum = 0;
		var comparer = new StructEqualityComparer();
		foreach (var i in array1.AsOptiQuery().Union(array2.AsOptiQuery(), comparer))
		{
			sum += i.Element;
		}

		return sum;
	}
}