using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct OrderEnumerator<T, TBaseQuery, TBaseEnumerator> : IOptiEnumerator<T>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private readonly Buffer<T, TBaseQuery, TBaseEnumerator> _buffer;
	private readonly int[] _map;
	private int _index = 0;

	internal OrderEnumerator(Buffer<T, TBaseQuery, TBaseEnumerator> buffer, int[] map)
	{
		_buffer = buffer;
		_map = map;
	}

	public T Current => _buffer._items[_map[_index]];

	public bool MoveNext()
	{
		if (_index < _map.Length - 1)
		{
			_index++;
			return true;
		}

		return false;
	}
	
	public void Dispose()
	{
		
	}
}