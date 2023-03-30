using System.Collections;

namespace OptiLinq;

public struct ArrayEnumerator<T> : IEnumerator<T>
{
	private readonly T[] _array;
	private int _index = 0;
	private T _current;

	internal ArrayEnumerator(T[] list)
	{
		_array = list;
		_index = -1;
	}

	object IEnumerator.Current => Current;

	public T Current => _current;

	public bool MoveNext()
	{
		if ((uint)_index < (uint)_array.Length)
		{
			_current = _array[_index++];
			return true;
		}

		return false;
	}

	public void Reset()
	{
		_index = 0;
	}

	public void Dispose()
	{
	}
}