using System.Buffers;
using System.Runtime.CompilerServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ReverseEnumerator<T, TBaseEnumerator> : IOptiEnumerator<T> where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private readonly T[] _data;
	private readonly int _count = 0;
	private int _index = -1;

	public ReverseEnumerator(TBaseEnumerator enumerator, int initialCount)
	{
		_data = EnumerableHelper.ToArray(enumerator, ArrayPool<T>.Shared, initialCount, out _count);
		Array.Reverse(_data, 0, _count);
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
		ArrayPool<T>.Shared.Return(_data, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
	}
}