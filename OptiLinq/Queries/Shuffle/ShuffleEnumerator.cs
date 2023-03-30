using System.Collections;
using OptiLinq.Collections;

namespace OptiLinq;

public struct ShuffleEnumerator<T, TBaseEnumerator> : IEnumerator<T>
	where TBaseEnumerator : IEnumerator<T>
{
	private PooledList<T> _list;
	private int _index = 0;

	internal ShuffleEnumerator(PooledList<T> list)
	{
		_list = list;
	}

	object IEnumerator.Current => Current;

	public T Current { get; private set; }

	public bool MoveNext()
	{
		if (_index >= _list.Count)
		{
			return false;
		}

		Current = _list[_index];

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