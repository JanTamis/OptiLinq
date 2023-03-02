using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Select
{
	private const int Count = 10000;

	public Select()
	{
	}

	[Benchmark(Baseline = true)]
	public double SysSelect()
	{
		return Enumerable.Range(0, Count).Select(x => x * 2.0).Sum();
	}

	[Benchmark]
	public double DelegateSelect()
	{
		return OptiQuery
			.Range(0, Count)
			.Select(x => x * 2.0)
			.Sum();
	}

	[Benchmark]
	public double OptiLinqSelect()
	{
		return OptiQuery.Range(0, Count)
			.Select<MultFunction, double>()
			.Sum(x => x);
	}
}