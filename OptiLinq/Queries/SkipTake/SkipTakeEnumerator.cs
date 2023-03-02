using System.Numerics;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator> : IOptiEnumerator<T>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TSkipCount : IBinaryInteger<TSkipCount>
	where TTakeCount : IBinaryInteger<TTakeCount>
{
	private TBaseEnumerator _baseEnumerator;
	private TSkipCount _skipCount;
	private TTakeCount _takeCount;
	private bool _initialized = false;

	public SkipTakeEnumerator(TBaseEnumerator baseEnumerator, TSkipCount skipCount, TTakeCount takeCount)
	{
		_baseEnumerator = baseEnumerator;
		_skipCount = skipCount;
		_takeCount = takeCount;
	}

	public T Current => _baseEnumerator.Current;

	public bool MoveNext()
	{
		if (!_initialized)
		{
			for (var i = TSkipCount.Zero; i < _skipCount && _baseEnumerator.MoveNext(); i++)
			{
			}

			_initialized = true;
		}
		
		return _takeCount-- > TTakeCount.Zero && _baseEnumerator.MoveNext();
	}
	
	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}