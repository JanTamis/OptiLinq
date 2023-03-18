using System.Collections;
using OptiLinq.Collections;

namespace OptiLinq;

public struct ReverseEnumerator<T> : IEnumerator<T>
{
	private PooledList<T> _list;
	private int _index = -1;

	public ReverseEnumerator(PooledList<T> list)
	{
		_list = list;

		_list.Items
			.AsSpan()
			.Reverse();
	}

	object IEnumerator.Current => Current;

	public T Current => _list[_index];

	public bool MoveNext()
	{
		if (_index < _list.Count - 1)
		{
			_index++;
			return true;
		}

		return false;
	}

	public void Reset()
	{
		_index = -1;
	}
	
	public void Dispose()
	{
		_list.Dispose();
	}
}