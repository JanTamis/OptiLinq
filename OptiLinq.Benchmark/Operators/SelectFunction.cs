using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

public struct SelectFunction : IFunction<int, int>
{
	public readonly int Eval(in int element)
	{
		return element * 2;
	}
}