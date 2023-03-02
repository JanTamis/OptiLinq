using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class SelectOnArrayOfClass
{
	private Container[] array;
	private const int Count = 10000;

	public SelectOnArrayOfClass()
	{
		array = Enumerable.Range(0, Count).Select(x => new Container(x)).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Handmaded()
	{
		var sum = 0;

		foreach (var i in array)
		{
			sum += i.Element;
		}

		return sum;
	}

	[Benchmark]
	public int LINQ()
	{
		var sum = 0;

		foreach (var i in array.Select(x => x.Element))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLINQ()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().Select(x => x.Element))
		{
			sum += i;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLINQWithFunction()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().Select<ContainerSelect, int>())
		{
			sum += i;
		}

		return sum;
	}
}