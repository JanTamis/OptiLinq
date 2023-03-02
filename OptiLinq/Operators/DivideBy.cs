using System.Numerics;
using OptiLinq.Interfaces;

public readonly struct DivideBy<T> : IFunction<T, T> where T : INumberBase<T>
{
	private readonly T _factor;

	public DivideBy(T factor)
	{
		_factor = factor;
	}

	public T Eval(in T item)
	{
		return item / _factor;
	}
}