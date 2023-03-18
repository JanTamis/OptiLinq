using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ArrayEnumerator<T> : IEnumerator<T>
{
	private readonly T[] _array;
	private int _index;

	internal ArrayEnumerator(T[] list)
	{
		_array = list;
		_index = -1;
	}


	object IEnumerator.Current => Current;

	public T Current
	{
		get
		{
			var index = _index;
			var array = _array;

			if ((uint)index >= (uint)array.Length)
			{
				throw new IndexOutOfRangeException();
			}

			return array[index];
		}
	}

	public bool MoveNext()
	{
		var index = _index + 1;

		if ((uint)index >= (uint)_array.Length)
		{
			_index = _array.Length;
			return false;
		}

		_index = index;
		return true;
	}

	public void Reset()
	{
		_index = -1;
	}

	public void Dispose()
	{
	}
}