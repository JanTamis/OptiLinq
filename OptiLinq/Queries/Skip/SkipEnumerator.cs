using System.Collections;
using System.Numerics;

namespace OptiLinq;

public struct SkipEnumerator<TCount, T, TBaseEnumerator> : IEnumerator<T>
	where TBaseEnumerator : IEnumerator<T>
	where TCount : IBinaryInteger<TCount>
{
	private TBaseEnumerator _enumerator;
	private readonly TCount _count;
	private bool _initialized;

	internal SkipEnumerator(TBaseEnumerator enumerator, TCount count)
	{
		_enumerator = enumerator;
		_count = count;
	}

	object IEnumerator.Current => Current;

	public T Current => _enumerator.Current;

	public bool MoveNext()
	{
		if (!_initialized)
		{
			for (var i = TCount.Zero; i < _count && _enumerator.MoveNext(); i++)
			{
			}
		}

		return _enumerator.MoveNext();
	}

	public void Reset()
	{
		_enumerator.Reset();
		_initialized = false;
	}
	
	public void Dispose()
	{
		_enumerator.Dispose();
	}
}