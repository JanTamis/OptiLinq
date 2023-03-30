using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ToListComparison
{
	private RangeQuery enumerable;

	public ToListComparison()
	{
		enumerable = OptiQuery.Range(0, 10_000);
	}

	[Benchmark]
	public List<int> AddInList()
	{
		var list = new List<int>();
		var rangeEnumerator = enumerable.GetEnumerator();
		FillList(list, ref rangeEnumerator);
		return list;
	}

	[Benchmark]
	public List<int> ToList()
	{
		return enumerable.ToList();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void FillList<T, TEnumerator>(List<T> list, ref TEnumerator enumerator)
		where TEnumerator : IEnumerator<T>
	{
		while (enumerator.MoveNext())
		{
			var current = enumerator.Current;
			list.Add(current);
		}

		enumerator.Dispose();
	}
}