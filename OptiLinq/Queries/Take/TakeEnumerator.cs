using System.Collections;
using System.Numerics;

namespace OptiLinq;

public struct TakeEnumerator<TCount, T, TBaseEnumerator> : IEnumerator<T>
	where TBaseEnumerator : IEnumerator<T>
	where TCount : IBinaryInteger<TCount>
{
	private TBaseEnumerator _baseEnumerator;
	private readonly TCount _count;
	private TCount _currentCount;

	public TakeEnumerator(TBaseEnumerator baseEnumerator, TCount count)
	{
		_baseEnumerator = baseEnumerator;
		_count = count;
		_currentCount = _count;
	}

	object IEnumerator.Current => Current;

	public T Current => _baseEnumerator.Current;

	public bool MoveNext()
	{
		return _currentCount++ < _count && _baseEnumerator.MoveNext();
	}

	public void Reset()
	{
		_currentCount = _count;
		_baseEnumerator.Reset();
	}
	
	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}