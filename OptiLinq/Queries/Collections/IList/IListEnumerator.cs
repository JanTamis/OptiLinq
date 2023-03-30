using System.Collections;

namespace OptiLinq;

public struct IListEnumerator<T> : IEnumerator<T>
{
	private readonly IList<T> _list;
	private int _index;
	private T _current;

	public IListEnumerator(IList<T> list)
	{
		_list = list;
	}

	object IEnumerator.Current => Current;
	public T Current => _current;

	public bool MoveNext()
	{
		if ((uint)_index < (uint)_list.Count)
		{
			_current = _list[_index];
			_index++;
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