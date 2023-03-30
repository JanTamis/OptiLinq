using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class OrderByArrayOfBigStruct
{
	private const int Count = 10_000;
	private StructContainer[] array;

	public OrderByArrayOfBigStruct()
	{
		array = OptiQuery.Random(42)
			.Take(Count)
			.Select(StructContainer.Create)
			.ToArray();
	}

	[Benchmark(Baseline = true)]
	public int LINQ()
	{
		var sum = 0;

		foreach (var i in array.OrderBy(o => Int32.IsOddInteger(o.Element)).ThenBy(x => x.Element))
		{
			sum += i.Element;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqOrder()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().OrderBy<bool, StructContainerIsOdd>().ThenBy<int, StructContainerIdentity>())
		{
			sum += i.Element;
		}

		return sum;
	}

	[Benchmark]
	public int OptiLinqOrderFunc()
	{
		var sum = 0;

		foreach (var i in array.AsOptiQuery().OrderBy(o => Int32.IsOddInteger(o.Element)).ThenBy(x => x.Element))
		{
			sum += i.Element;
		}

		return sum;
	}

	private struct StructContainerIsOdd : IFunction<StructContainer, bool>
	{
		public bool Eval(in StructContainer element)
		{
			return Int32.IsOddInteger(element.Element);
		}
	}

	private struct StructContainerIdentity : IFunction<StructContainer, int>
	{
		public int Eval(in StructContainer element)
		{
			return element.Element;
		}
	}
}