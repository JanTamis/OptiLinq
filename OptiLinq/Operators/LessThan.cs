using OptiLinq.Interfaces;

namespace OptiLinq.Operators;

public readonly struct LessThan<T> : IFunction<T, bool>
{
	private readonly T _value;

	public LessThan(T value)
	{
		_value = value;
	}

	public bool Eval(in T element)
	{
		return Comparer<T>.Default.Compare(element, _value) < 0;
	}
}