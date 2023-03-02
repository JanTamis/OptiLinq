using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ArrayEnumerator<T> : IOptiEnumerator<T>
{
	private readonly T[] _array;
	private int _index;

	internal ArrayEnumerator(T[] list)
	{
		_array = list;
		_index = -1;
	}

	public T Current => _array[_index];

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

	public void Dispose()
	{
	}
}