using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

public readonly struct MultiplyBy<T> : IFunction<T, T>
	where T : INumberBase<T>
{
	private readonly T _factor;

	public MultiplyBy(T factor)
	{
		_factor = factor;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T Eval(in T item)
	{
		return item * _factor;
	}
}