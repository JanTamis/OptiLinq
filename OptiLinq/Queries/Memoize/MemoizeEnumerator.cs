using OptiLinq.Interfaces;

namespace OptiLinq;

public struct MemoizeEnumerator<T, TEnumerator> : IOptiEnumerator<T> 
	where TEnumerator : struct, IOptiEnumerator<T>
{
	private List<T>? _cache;
	private readonly object _locker;
	private TEnumerator _baseEnumerator;
	
	private bool _lockTaken;
	private readonly bool _useCache;
	
	private int _index = -1;

	internal MemoizeEnumerator(TEnumerator baseEnumerator, object locker, ref List<T>? cache)
	{
		_baseEnumerator = baseEnumerator;
		_locker = locker;
		_cache = cache;
		
		_useCache = _cache is not null;
	}
	
	public T Current { get; private set; }

	public bool MoveNext()
	{
		if (!_lockTaken)
		{
			Monitor.Enter(_locker, ref _lockTaken);
		}

		if (_useCache && _cache is not null)
		{
			if (++_index < _cache.Count)
			{
				Current = _cache[_index];
				return true;
			}
		}
		else
		{
			_cache ??= new List<T>();
			
			if (_baseEnumerator.MoveNext())
			{
				Current = _baseEnumerator.Current;
				_cache?.Add(Current);
				
				return true;
			}
		}

		Current = default;
		return false;
	}
	
	public void Dispose()
	{
		Monitor.Exit(_locker);
	}
}