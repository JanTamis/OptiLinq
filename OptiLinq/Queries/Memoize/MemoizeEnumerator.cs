using System.Collections;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct MemoizeEnumerator<T, TEnumerator> : IEnumerator<T>
	where TEnumerator : IEnumerator<T>
{
	private PooledList<T> _cache;
	private TEnumerator _baseEnumerator;
	
	private bool _lockTaken;
	private readonly bool _useCache;
	private readonly object _locker;
	
	private int _index = -1;

	internal MemoizeEnumerator(TEnumerator baseEnumerator, object locker, ref PooledList<T> cache, bool useCache)
	{
		_baseEnumerator = baseEnumerator;
		_locker = locker;
		_cache = cache;

		_useCache = useCache;
	}

	object IEnumerator.Current => Current;
	
	public T Current { get; private set; }

	public bool MoveNext()
	{
		if (!_lockTaken)
		{
			Monitor.Enter(_locker, ref _lockTaken);
		}

		if (_useCache)
		{
			if (++_index < _cache.Count)
			{
				Current = _cache[_index];
				return true;
			}
		}
		else
		{
			if (_baseEnumerator.MoveNext())
			{
				_cache.Add(_baseEnumerator.Current);
				
				return true;
			}
		}

		Current = default;
		return false;
	}

	public void Reset()
	{
		_index = -1;
		_baseEnumerator.Reset();
	}
	
	public void Dispose()
	{
		Monitor.Exit(_locker);
	}
}