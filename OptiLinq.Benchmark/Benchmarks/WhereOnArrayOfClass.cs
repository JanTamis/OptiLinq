using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class WhereOnArrayOfClass
{
	private Container[] array;
	private const int Count = 10000;

	public WhereOnArrayOfClass()
	{
		array = Enumerable.Range(0, Count).Select(x => new Container(x)).ToArray();
	}

	[Benchmark(Baseline = true)]
	public int Handmaded()
	{
		var sum = 0;

		foreach (var i in array)
		{
			if (i.Element % 2 == 0)
				sum += i.Element;
		}

		return sum;
	}

	[Benchmark]
	public int LINQ()
	{
		var sum = 0;

		foreach (var i in array.Where(x => x.Element % 2 == 0))
		{
			sum += i.Element;
		}

		return sum;
	}

	[Benchmark]
	public int LinqSum()
	{
		return array.Where(x => x.Element % 2 == 0).Sum(x => x.Element);
	}

	[Benchmark]
	public int OptiLinq()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().Where(x => x.Element % 2 == 0))
		{
			sum += i.Element;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqSum()
	{
		return array.AsOptiQuery().Where(x => x.Element % 2 == 0).Sum(x => x.Element);
	}

	[Benchmark]
	public int OptiLinqWithFunction()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().Where<WhereFunc>())
		{
			sum += i.Element;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqWithFunctionSum()
	{
		return array.AsOptiQuery().Where<WhereFunc>().Sum(x => x.Element);
	}

	private readonly struct WhereFunc : IFunction<Container, bool>
	{
		public bool Eval(in Container element)
		{
			return Int32.IsEvenInteger(element.Element);
		}
	}
}