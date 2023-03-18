using System.Collections;

namespace OptiLinq;

public readonly struct RepeatEnumerator<T> : IEnumerator<T>
{
	public RepeatEnumerator(T element)
	{
		Current = element;
	}

	object IEnumerator.Current => Current;

	public T Current { get; }

	public bool MoveNext() => true;

	public void Reset()
	{
	}

	public void Dispose()
	{
		
	}
}