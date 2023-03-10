using System.Buffers;
using System.Runtime.CompilerServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct OrderEnumerator<T, TComparer, TBaseEnumerator> : IOptiEnumerator<T>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TComparer : IComparer<T>
{
	private readonly T[] _data;
	private readonly int _count = 0;
	private int _index = -1;

	private ArrayPool<T> _arrayPool;

	internal OrderEnumerator(TBaseEnumerator enumerator, TComparer comparer, int initialCount)
	{
		_arrayPool = ArrayPool<T>.Shared;

		_data = EnumerableHelper.ToArray(enumerator, _arrayPool, initialCount, out _count);
		_data.AsSpan(0, _count).Sort(comparer);

		enumerator.Dispose();
	}

	public T Current => _data[_index];

	public bool MoveNext()
	{
		if (_index < _data.Length - 1)
		{
			_index++;
			return true;
		}

		return false;
	}

	public void Dispose()
	{
		try
		{
			_arrayPool.Return(_data, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
		}
		catch (Exception e)
		{
			// ignored
		}
	}
}