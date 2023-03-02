using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

readonly struct MultFunction : IFunction<int, double>
{
	public double Eval(in int element)
	{
		return element * 2.0;
	}
}