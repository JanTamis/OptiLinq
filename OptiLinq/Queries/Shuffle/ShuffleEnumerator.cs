using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ShuffleEnumerator<T, TBaseEnumerator> : IOptiEnumerator<T>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;
	private PooledList<T> _list;
	private readonly Random _random;
	private int _index = -1;

	internal ShuffleEnumerator(TBaseEnumerator baseEnumerator, Random random, int capacity = 0)
	{
		_baseEnumerator = baseEnumerator;
		_random = random;
		_list = new PooledList<T>(capacity);
	}

	public T Current => _list[_index];

	public bool MoveNext()
	{
		if (_index == -1)
		{
			_list = new PooledList<T>();

			while (_baseEnumerator.MoveNext())
			{
				_list.Add(_baseEnumerator.Current);
			}

			for (var i = _list.Count - 1; i >= 1; i--)
			{
				var j = _random.Next(i + 1);

				(_list[j], _list[i]) = (_list[i], _list[j]);
			}

			_baseEnumerator.Dispose();
		}

		if (_index >= _list.Count)
		{
			return false;
		}

		_index++;

		return true;
	}

	public void Dispose()
	{
		_list.Dispose();
	}
}