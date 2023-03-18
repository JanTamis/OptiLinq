using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ArrayOfClassSum
{
	private const int Count = 1000;
	private readonly Container[] array;

	public ArrayOfClassSum()
	{
		array = Enumerable.Range(0, Count).Select(x => new Container(x)).ToArray();
	}

	[Benchmark]
	public int Handmaded()
	{
		var sum = 0;

		for (var i = 0; i < Count; i++)
		{
			sum += array[i].Element;
		}

		return sum;
	}

	[Benchmark(Baseline = true)]
	public int LINQSum() => array.Select(x => x.Element).Sum();

	[Benchmark]
	public int DelegateSum()
	{
		return array.AsOptiQuery()
			.Select(x => x.Element)
			.Sum();
	}

	[Benchmark]
	public int DelegateWithoutSelectSum()
	{
		return array.AsOptiQuery()
			.Sum(s => s.Element);
	}

	[Benchmark]
	public int IFunctionSum()
	{
		return array.AsOptiQuery()
			.Sum<Container, int, ContainerSelect>();
	}
}

public class Container
{
	public readonly int Element;

	public Container(int element)
	{
		Element = element;
	}
}

internal struct ContainerSelect : IFunction<Container, int>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly int Eval(in Container element)
	{
		return element.Element;
	}
}