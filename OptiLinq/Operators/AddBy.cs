using System.Numerics;
using OptiLinq.Interfaces;

public readonly struct AddBy<T> : IFunction<T, T>
	where T : INumberBase<T>
{
	private readonly T _factor;

	public AddBy(T factor)
	{
		_factor = factor;
	}

	public T Eval(in T item)
	{
		return item + _factor;
	}
}