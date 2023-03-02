using System.Numerics;
using OptiLinq.Interfaces;

public readonly struct SquareRoot<T> : IFunction<T, T> where T : IRootFunctions<T>
{
	public T Eval(in T item)
	{
		return T.Sqrt(item);
	}
}