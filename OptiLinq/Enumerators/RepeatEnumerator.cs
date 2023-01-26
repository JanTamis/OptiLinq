using OptiLinq.Interfaces;

namespace OptiLinq;

public struct RepeatEnumerator<T> : IOptiEnumerator<T>
{
	private int _count;

	public RepeatEnumerator(T element, int count)
	{
		Current = element;
		_count = count;
	}

	public T Current { get; }

	public void Dispose()
	{
		
	}

	
	public bool MoveNext()
	{
		if (_count > 0)
		{
			_count--;
			return true;
		}

		return false;
	}
}