using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class RangeWhereSelectSum
{
	private const int Count = 10000;

	public RangeWhereSelectSum()
	{
	}

	[Benchmark(Baseline = true)]
	public int SysSum()
	{
		int sum = 0;
		for (int i = 0; i < Count; i++)
		{
			if ((i & 1) == 0)
			{
				var mult = i * 2;
				sum += mult;
			}
		}

		return sum;
	}

	[Benchmark]
	public int SysRangeWhereSelectSum() => Enumerable.Range(0, Count)
		.Where(x => (x & 1) == 0)
		.Select(x => x * 2)
		.Sum();

	[Benchmark]
	public int ConvertWhereSelectSumWithDelegate()
	{
		return Enumerable.Range(0, Count)
			.AsOptiQuery()
			.Where(x => (x & 1) == 0)
			.Select(x => x * 2)
			.Sum(x => x);
	}

	[Benchmark]
	public int OptiRangeWhereSelectSum()
	{
		return OptiQuery.Range(0, Count)
			.Where<IsEven<int>>()
			.Select<SelectFunction>()
			.Sum();
	}
}