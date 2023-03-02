using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[Config(typeof(BenchmarkConfig))]
public class ForEach
{
	private int count;
	private Action<int> action;
	private const int Count = 100000;

	public ForEach()
	{
		count = 0;
		action = i => count++;
	}

	[Benchmark(Baseline = true)]
	public int ClrForEach()
	{
		var sysRange = Enumerable.Range(0, Count);
		foreach (var i in sysRange)
		{
			count++;
		}

		return count;
	}

	[Benchmark]
	public int WithAction()
	{
		OptiQuery.Range(0, Count).ForEach(action);
		return count;
	}

	[Benchmark]
	public int WithStruct()
	{
		var countAction = new CountAction<int> { Count = 0 };
		OptiQuery.Range(0, Count).ForEach(countAction);

		return countAction.Count;
	}


	[Benchmark]
	public int ToTypedEnumerableWithStruct()
	{
		var countAction = new CountAction<int> { Count = 0 };

		var convertRange = Enumerable.Range(0, Count).AsOptiQuery();
		convertRange.ForEach(countAction);
		return countAction.Count;
	}
}

struct CountAction<T> : IAction<T>
{
	public int Count;

	public void Do(in T element)
	{
		Count++;
	}
}