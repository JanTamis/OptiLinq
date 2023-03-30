using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ToArrayComparison
{
	private RangeQuery enumerable;

	public ToArrayComparison()
	{
		enumerable = OptiQuery.Range(0, 10_000);
	}

	[Benchmark]
	public int[] ToListThenToArray()
	{
		var list = new List<int>();
		foreach (var i in enumerable)
		{
			list.Add(i);
		}

		return list.ToArray();
	}

	[Benchmark]
	public int[] UseCountForToArray()
	{
		var enumerator = enumerable.GetEnumerator();
		return ToArray<int, RangeEnumerator>(ref enumerator, enumerable.Count());
	}

	[Benchmark]
	public int[] OptiLinqToArray()
	{
		return enumerable.ToArray();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static T[] ToArray<T, TEnumerator>(ref TEnumerator enumerator, int size)
		where TEnumerator : IEnumerator<T>
	{
		var result = new T[size];
		var i = 0;
		
		while (enumerator.MoveNext())
		{
			result[i++] = enumerator.Current;
		}

		enumerator.Dispose();
		return result;
	}
}