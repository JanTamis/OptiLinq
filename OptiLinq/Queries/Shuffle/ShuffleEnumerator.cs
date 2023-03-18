using System.Collections;
using OptiLinq.Collections;

namespace OptiLinq;

public struct ShuffleEnumerator<T, TBaseEnumerator> : IEnumerator<T>
	where TBaseEnumerator : IEnumerator<T>
{
	private PooledList<T> _list;
	private int _index = -1;

	internal ShuffleEnumerator(PooledList<T> list)
	{
		_list = list;
	}

	object IEnumerator.Current => Current;

	public T Current => _list[_index];

	public bool MoveNext()
	{
		if (_index >= _list.Count)
		{
			return false;
		}

		_index++;

		return true;
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