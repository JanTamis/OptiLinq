using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class TakeWhileOnArray
{
	private const int Count = 10_000;
	public int[] array;

	public TakeWhileOnArray()
	{
		array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Linq()
	{
		var sum = 0;
		foreach (var i in array.TakeWhile(x => x > 5000))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int LinqSum()
	{
		return array.TakeWhile(x => x > 5000).Sum();
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;
		foreach (var i in array.AsOptiQuery().TakeWhile(x => x > 5000))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqSum()
	{
		return array.AsOptiQuery().TakeWhile(x => x > 5000).Sum();
	}

	[Benchmark]
	public int OptiLinqFunction()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().TakeWhile<Predicate>())
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqFunctionSum()
	{
		return array.AsOptiQuery().TakeWhile<Predicate>().Sum();
	}

	private struct Predicate : IFunction<int, bool>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Eval(in int element)
		{
			return element > 5000;
		}
	}
}