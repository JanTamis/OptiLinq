using System.Numerics;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SkipEnumerator<TCount, T, TBaseEnumerator> : IOptiEnumerator<T>
	where TBaseEnumerator : IOptiEnumerator<T>
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
	
	public void Dispose()
	{
		_enumerator.Dispose();
	}
}