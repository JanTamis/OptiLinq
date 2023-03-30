using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class AllOnArray
{
	private const int Count = 1000;
	private readonly int[] array;

	public AllOnArray()
	{
		array = Enumerable.ToArray(Enumerable.Range(0, Count));
	}

	[Benchmark]
	public bool For()
	{
		var ints = array;
		var arrayLength = ints.Length;

		for (var i = 0; i < arrayLength; i++)
		{
			if (ints[i] >= arrayLength / 2)
				return false;
		}

		return true;
	}

	[Benchmark(Baseline = true)]
	public bool Linq() => array.All(a => a < Count / 2);

	[Benchmark]
	public bool DelegateOptiLinq() => array.AsOptiQuery().All(a => a < Count / 2);

	[Benchmark]
	public bool IFunctionOptiLinq()
	{
		return array.AsOptiQuery().All<AllFunction>();
	}

	private struct AllFunction : IFunction<int, bool>
	{
		public bool Eval(in int element)
		{
			return element < Count / 2;
		}
	}
}