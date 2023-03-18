using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class Aggregate
{
	private readonly RangeQuery _rangeEnumerable;
	private readonly IEnumerable<int> _enumerable;
	private const int Count = 10000;

	public Aggregate()
	{
		_rangeEnumerable = OptiQuery.Range(0, Count);
		_enumerable = Enumerable.Range(0, Count);
	}

	[Benchmark(Baseline = true)]
	public int SysAggregate()
	{
		return _enumerable.Aggregate(0, (accu, elt) => accu + elt);
	}

	[Benchmark]
	public int DelegateAggregate()
	{
		return _rangeEnumerable.Aggregate((accu, elt) => accu + elt, 0);
	}

	[Benchmark]
	public int IFunctionAggregate()
	{
		return _rangeEnumerable.Aggregate<Aggregation, int>();
	}


	[Benchmark]
	public int ConvertAggregate()
	{
		return _enumerable.AsOptiQuery().Aggregate<Aggregation, int>();
	}
}

struct Aggregation : IAggregateFunction<int, int, int>
{
	public int Eval(in int accumulator, in int element)
	{
		return accumulator + element;
	}
}