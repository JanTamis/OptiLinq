using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using OptiLinq.Interfaces;

namespace OptiLinq;

public readonly struct RangeQuery : IOptiQuery<int, RangeEnumerator>
{
	private readonly int _start, _count;

	internal RangeQuery(int start, int count)
	{
		// if (count < 0)
		// {
		// 	throw new ArgumentOutOfRangeException(nameof(count), "Count must be non non negative");
		// }
		_start = start;
		_count = count;
	}

	public bool Any()
	{
		return _count != 0;
	}

	public bool Contains(int item, IEqualityComparer<int>? comparer = null)
	{
		if (comparer is null)
		{
			return item >= _start && item <= _count + _start;
		}

		for (var i = _start; i < _start + _count; i++)
		{
			if (comparer.Equals(item, i))
			{
				return true;
			}
		}

		return false;

	}

	public int Count()
	{
		return _count;
	}

	public int First()
	{
		return _start;
	}

	public int FirstOrDefault()
	{
		return _start;
	}

	public void CopyTo(Span<int> data)
	{
		var index = 0;
		var count = data.Length;

		ref var startRef = ref MemoryMarshal.GetReference(data);

		if (Vector256.IsHardwareAccelerated && index + Vector256<int>.Count < count)
		{
			var mask = Vector256.Create(Vector256<int>.Count);
			var buffer = Vector256.Create(0, 1, 2, 3, 4, 5, 6, 7) + Vector256.Create(_start + index);

			do
			{
				buffer.StoreUnsafe(ref Unsafe.Add(ref startRef, index));
				buffer += mask;

				index += Vector256<int>.Count;
			} while (index < count);
		}

		if (Vector128.IsHardwareAccelerated && index + Vector128<int>.Count < count)
		{
			var mask = Vector128.Create(Vector128<int>.Count);
			var buffer = Vector128.Create(0, 1, 2, 3) + Vector128.Create(_start + index);

			do
			{
				buffer.StoreUnsafe(ref Unsafe.Add(ref startRef, index));
				buffer += mask;

				index += Vector128<int>.Count;
			} while (index < count);
		}

		if (Vector64.IsHardwareAccelerated && index + Vector64<int>.Count < count)
		{
			var mask = Vector64.Create(Vector64<int>.Count);
			var buffer = Vector64.Create(0, 1) + Vector64.Create(_start + index);

			do
			{
				buffer.StoreUnsafe(ref Unsafe.Add(ref startRef, index));
				buffer += mask;

				index += Vector64<int>.Count;
			} while (index < count);
		}

		var value = index + _start;

		while (index < count)
		{
			Unsafe.Add(ref startRef, index++) = value++;
		}
	}

	public int[] ToArray()
	{
		var array = GC.AllocateUninitializedArray<int>(_count);

		CopyTo(array);

		return array;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = _count;
		return true;
	}

	public WhereQuery<int, TOperator, RangeQuery, RangeEnumerator> Where<TOperator>() where TOperator : IWhereOperator<int>
	{
		return new WhereQuery<int, TOperator, RangeQuery, RangeEnumerator>(this);
	}

	public SelectQuery<int, TResult, TOperator, RangeQuery, RangeEnumerator> Select<TResult, TOperator>()
		where TOperator : ISelectOperator<int, TResult>
	{
		return new SelectQuery<int, TResult, TOperator, RangeQuery, RangeEnumerator>(this);
	}

	public RangeEnumerator GetEnumerator()
	{
		return new RangeEnumerator(_start, _count);
	}
}