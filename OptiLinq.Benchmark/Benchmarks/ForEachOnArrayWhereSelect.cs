using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ForEachOnArrayWhereSelect
{
	private const int Count = 10000;
	private readonly int[] array;

	public ForEachOnArrayWhereSelect()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}


	[Benchmark]
	public int SysLinq()
	{
		var enumerable = array
			.Where(x => (x & 1) == 0)
			.Select(x => x * 2);
		var sum = 0;
		foreach (var i in enumerable)
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqWithDelegate()
	{
		var enumerable = array
			.AsOptiQuery()
			.Where(x => (x & 1) == 0)
			.Select(x => x * 2);

		var sum = 0;
		foreach (var i in enumerable)
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqWithDelegateAsEnumerable()
	{
		var enumerable = array
			.AsOptiQuery()
			.Where(x => (x & 1) == 0)
			.Select(x => x * 2);

		var sum = 0;
		foreach (var i in enumerable.AsEnumerable())
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinq()
	{
		var enumerable = array
			.AsOptiQuery()
			.Where<IsEven<int>>()
			.Select<SelectFunction>();

		var sum = 0;
		foreach (var i in enumerable)
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqAsEnumerable()
	{
		var enumerable = array
			.AsOptiQuery()
			.Where<IsEven<int>>()
			.Select<SelectFunction>();

		var sum = 0;
		foreach (var i in enumerable.AsEnumerable())
		{
			sum += i;
		}

		return sum;
	}
}