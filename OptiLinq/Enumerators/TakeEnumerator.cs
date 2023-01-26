using System.Numerics;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct TakeEnumerator<TCount, T, TBaseEnumerator> : IOptiEnumerator<T>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TCount : IBinaryInteger<TCount>
{
	private TBaseEnumerator _baseEnumerator;
	private TCount _count;

	public TakeEnumerator(TBaseEnumerator baseEnumerator, TCount count)
	{
		_baseEnumerator = baseEnumerator;
		_count = count;
	}


	public T Current => _baseEnumerator.Current;

	public bool MoveNext()
	{
		return _count-- > TCount.Zero && _baseEnumerator.MoveNext();
	}
	
	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}