using OptiLinq.Interfaces;

namespace OptiLinq;

public struct RepeatEnumerator<T> : IOptiEnumerator<T>
{
	public RepeatEnumerator(T element)
	{
		Current = element;
	}

	public T Current { get; }

	public bool MoveNext() => true;

	public void Dispose()
	{
		
	}
}