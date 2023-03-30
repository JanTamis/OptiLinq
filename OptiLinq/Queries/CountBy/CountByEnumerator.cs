using System.Collections;
using System.Numerics;
using OptiLinq.Collections;

namespace OptiLinq;

public struct CountByEnumerator<TKey, TCount, TComparer> : IEnumerator<KeyValuePair<TKey, TCount>>
	where TComparer : IEqualityComparer<TKey>
	where TCount : INumberBase<TCount>
	where TKey : notnull
{
	private PooledDictionary<TKey, TCount, TComparer> _dictionary;

	private int _index;

	public CountByEnumerator(PooledDictionary<TKey, TCount, TComparer> dictionary)
	{
		_dictionary = dictionary;
		_index = 0;
	}

	public bool MoveNext()
	{
		if (_index < _dictionary.Count)
		{
			var slotItem = _dictionary._slots[_index];
			Current = new KeyValuePair<TKey, TCount>(slotItem.Key, slotItem.Value);

			_index++;

			return true;
		}

		return false;
	}

	public void Reset()
	{
		_index = 0;
	}

	public KeyValuePair<TKey, TCount> Current { get; private set; }

	object IEnumerator.Current => Current;

	public void Dispose()
	{
		_dictionary.Dispose();
	}
}