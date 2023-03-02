using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class AnyOnArray
{
	private const int Count = 1000;
	private readonly int[] array;

	public AnyOnArray()
	{
		array = Enumerable.ToArray(Enumerable.Range(0, Count));
	}

	[Benchmark]
	public bool For()
	{
		for (var i = 0; i < Count; i++)
		{
			if (array[i] >= Count / 2)
				return true;
		}

		return false;
	}

	[Benchmark(Baseline = true)]
	public bool Linq() => array.Any(x => x >= Count / 2);

	[Benchmark]
	public bool DelegateOptiLinq() => array.AsOptiQuery().Any(x => x >= Count / 2);

	[Benchmark]
	public bool IFunctionOptiLinq()
	{
		return array.AsOptiQuery().Any<AllFunction>();
	}

	private struct AllFunction : IFunction<int, bool>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Eval(in int element)
		{
			return element >= Count / 2;
		}
	}
}