using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ToListOnArrayWhere
{
	private const int Count = 10000;
	private readonly int[] array;

	public ToListOnArrayWhere()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public List<int> Linq()
	{
		return array
			.Where(x => (x & 1) == 0)
			.ToList();
	}


	[Benchmark]
	public List<int> OptiLinq()
	{
		return array
			.AsOptiQuery()
			.Where(x => (x & 1) == 0)
			.ToList();
	}


	[Benchmark]
	public List<int> OptiLinqWithFunction()
	{
		return array
			.AsOptiQuery()
			.Where<IsEven<int>>()
			.ToList();
	}
}