using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ForEachOnListWithSelect
{
	private readonly List<int> list;

	public ForEachOnListWithSelect()
	{
		list = Enumerable.Range(-1, 10000).ToList();
	}

	[Benchmark(Baseline = true)]
	public int LINQ()
	{
		var sum = 0;
		foreach (var i in list.Select(x => x * 2))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqWithFunc()
	{
		var sum = 0;
		foreach (var i in list.AsOptiQuery().Select(x => x * 2))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqWithFuncAsEnumerable()
	{
		var sum = 0;
		foreach (var i in list.AsOptiQuery().Select(x => x * 2).AsEnumerable())
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqWithStructFunc()
	{
		var sum = 0;

		foreach (var i in list.AsOptiQuery().Select<Mult2, int>())
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqWithStructFuncAsEnumerable()
	{
		var sum = 0;

		foreach (var i in list.AsOptiQuery().Select<Mult2, int>().AsEnumerable())
		{
			sum += i;
		}

		return sum;
	}


	public readonly struct Mult2 : IFunction<int, int>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int Eval(in int element)
		{
			return element * 2;
		}
	}
}