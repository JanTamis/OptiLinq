using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Max
{
	private const int Count = 10000;

	private readonly int[] _array;

	public Max()
	{
		_array = Enumerable.Range(0, Count).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Handmaded()
	{
		var max = int.MinValue;

		for (var i = 0; i < _array.Length; i++)
		{
			var item = _array[i];

			if (item > max)
				max = item;
		}

		return max;
	}

	[Benchmark]
	public int LINQ()
	{
		return _array.Max();
	}

	[Benchmark]
	public int OptiLinq()
	{
		return _array.AsOptiQuery().Max();
	}
}