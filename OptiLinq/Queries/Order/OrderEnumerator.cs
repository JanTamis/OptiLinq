using System.Collections;
using OptiLinq.Collections;

namespace OptiLinq;

public struct OrderEnumerator<T> : IEnumerator<T>
{
	private int _index = -1;

	private PooledList<T> _data;

	public OrderEnumerator(PooledList<T> data)
	{
		_data = data;
	}

	object IEnumerator.Current => Current;

	public T Current { get; private set; } = default!;

	public bool MoveNext()
	{
		_index++;

		if (_index < _data.Count)
		{
			Current = _data.GetRef(_index);
			return true;
		}

		Current = default!;
		return false;
	}

	public void Reset()
	{
		_index = -1;
	}

	public void Dispose()
	{
		_data.Dispose();
	}
}