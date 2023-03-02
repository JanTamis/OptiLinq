using OptiLinq.Interfaces;

namespace OptiLinq.Operators;

public readonly struct IdentitySelector<T> : IFunction<T, T>
{
	public T Eval(in T element)
	{
		return element;
	}
}