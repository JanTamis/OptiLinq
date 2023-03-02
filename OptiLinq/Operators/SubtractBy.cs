using System.Numerics;
using OptiLinq.Interfaces;

public readonly struct SubtractBy<T> : IFunction<T, T> where T : INumberBase<T>
{
	private readonly T _factor;

	public SubtractBy(T factor)
	{
		_factor = factor;
	}

	public T Eval(in T item)
	{
		return item - _factor;
	}
}