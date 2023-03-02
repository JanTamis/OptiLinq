using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ToArrayOnArrayWhereOfClass
{
	private const int Count = 10000;
	private readonly Container[] array;

	public ToArrayOnArrayWhereOfClass()
	{
		array = Enumerable.Range(0, Count).Select(x => new Container(x)).ToArray();
	}

	[Benchmark(Baseline = true)]
	public Container[] Linq()
	{
		return array
			.Where(x => (x.Element & 1) == 0)
			.ToArray();
	}


	[Benchmark]
	public Container[] OptiLinq()
	{
		return array
			.AsOptiQuery()
			.Where(x => (x.Element & 1) == 0)
			.ToArray();
	}

	[Benchmark]
	public Container[] OptiLinqWithFunction()
	{
		return array
			.AsOptiQuery()
			.Where<WhereContainerPredicate>()
			.ToArray();
	}
}

public readonly struct WhereContainerPredicate : IFunction<Container, bool>
{
	public bool Eval(in Container element)
	{
		return (element.Element & 1) == 0;
	}
}