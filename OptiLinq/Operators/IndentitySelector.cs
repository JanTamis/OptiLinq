using OptiLinq.Interfaces;

namespace OptiLinq;

public struct Identity<T> : IFunction<T, T>
{
	public T Eval(in T element)
	{
		return element;
	}
}