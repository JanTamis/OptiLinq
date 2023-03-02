using OptiLinq.Interfaces;

namespace OptiLinq.Operators;

public readonly struct Equals<T> : IFunction<T, bool>
{
	private readonly T _value;

	public Equals(T value)
	{
		_value = value;
	}

	public bool Eval(in T element)
	{
		return EqualityComparer<T>.Default.Equals(element, _value);
	}
}