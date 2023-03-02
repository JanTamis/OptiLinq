using System.Runtime.InteropServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ListEnumerator<T> : IOptiEnumerator<T>
{
	private readonly List<T> _list;
	private int _index = 0;

	internal ListEnumerator(List<T> list)
	{
		_list = list;
	}

	public T Current { get; private set; }

	public bool MoveNext()
	{
		var data = CollectionsMarshal.AsSpan(_list);

		if ((uint)_index < (uint)data.Length)
		{
			Current = data[_index];
			_index++;

			return true;
		}

		return false;
	}

	public void Dispose()
	{
	}
}