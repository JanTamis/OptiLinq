using OptiLinq.Interfaces;

namespace OptiLinq.Operators;

public readonly struct GreaterThan<T> : IFunction<T, bool>
{
	private readonly T _value;

	public GreaterThan(T value)
	{
		_value = value;
	}

	public bool Eval(in T element)
	{
		return Comparer<T>.Default.Compare(element, _value) > 0;
	}
}