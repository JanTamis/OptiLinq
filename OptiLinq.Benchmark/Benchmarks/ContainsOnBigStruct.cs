using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ContainsOnBigStruct
{
	private const int Count = 10_000;
	private readonly StructContainer[] array;
	private readonly IEnumerable<StructContainer> enumerable;

	public ContainsOnBigStruct()
	{
		array = Enumerable.Range(0, Count).Select(StructContainer.Create).ToArray();
		enumerable = Enumerable.Range(0, Count).Select(StructContainer.Create).ToArray();
	}

	[Benchmark(Baseline = true)]
	public bool Linq()
	{
		return enumerable.Contains(StructContainer.Create(5_000));
	}

	[Benchmark]
	public bool Array()
	{
		return array.Contains(StructContainer.Create(5_000));
	}

	[Benchmark]
	public bool OptiLinq()
	{
		return array.AsOptiQuery().Contains(StructContainer.Create(5_000));
	}

	[Benchmark]
	public bool OptiLinqWithCustomComparer()
	{
		var comparer = new StructEqualityComparer();
		return array.AsOptiQuery().Contains(StructContainer.Create(5_000), comparer);
	}
}