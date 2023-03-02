using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Where
{
	private const int Count = 10000;

	[Benchmark(Baseline = true)]
	public int SysSelect()
	{
		return Enumerable.Range(0, Count).Where(x => x > 0).Sum();
	}

	[Benchmark]
	public int DelegateSelect()
	{
		return OptiQuery.Range(0, Count)
			.Where(x => x > 0)
			.Sum();
	}

	[Benchmark]
	public int StructSelect()
	{
		return OptiQuery.Range(0, Count).Where<IsEven<int>>().Sum();
	}
}