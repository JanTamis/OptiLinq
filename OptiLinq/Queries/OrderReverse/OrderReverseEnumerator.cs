using System.Buffers;
using System.Runtime.CompilerServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct OrderReverseEnumerator<T, TComparer, TBaseEnumerator> : IOptiEnumerator<T>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TComparer : IComparer<T>
{
	private readonly T[] _data;
	private readonly int _count = 0;
	private int _index = -1;

	internal OrderReverseEnumerator(TBaseEnumerator enumerator, TComparer comparer, int initialCount)
	{
		_data = EnumerableHelper.ToArray(enumerator, ArrayPool<T>.Shared, initialCount, out _count);
		
		var span = _data.AsSpan(0, _count);
		span.Sort(comparer);
		span.Reverse();
		
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
			ArrayPool<T>.Shared.Return(_data, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
		}
		catch (Exception e)
		{
			
		}
	}
}