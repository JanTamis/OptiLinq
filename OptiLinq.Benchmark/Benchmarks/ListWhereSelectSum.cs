using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ListWhereSelectSum
{
	private const int Count = 10000;
	private readonly List<int> list;

	public ListWhereSelectSum()
	{
		list = Enumerable.Range(0, Count).ToList();
	}


	[Benchmark(Baseline = true)]
	public int LINQ()
	{
		return list
			.Where(x => (x & 1) == 0)
			.Select(x => x * 2)
			.Sum();
	}

	[Benchmark]
	public int OptiLinqWithDelegate()
	{
		return list
			.AsOptiQuery()
			.Where(x => (x & 1) == 0)
			.Select(x => x * 2)
			.Sum();
	}


	[Benchmark]
	public int OptiLinqIFunction()
	{
		return list
			.AsOptiQuery()
			.Where<IsEven<int>>()
			.Select<SelectFunction>()
			.Sum(x => x);
	}
}