using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class TakeOnArray
{
	private const int Count = 10000;
	private const int TakeCount = 5000;
	private readonly int[] _array;

	public TakeOnArray()
	{
		_array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Linq()
	{
		var sum = 0;

		foreach (var i in _array.Take(TakeCount))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int LinqSum()
	{
		return _array.Take(TakeCount).Sum();
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;

		foreach (var i in _array.AsOptiQuery().Take(TakeCount))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqSum()
	{
		return _array.AsOptiQuery().Take(TakeCount).Sum();
	}
}