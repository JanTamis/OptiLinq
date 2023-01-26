using System.Numerics;
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
		if (count < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(count), "Count must be non non negative");
		}
		
		_start = start;
		_count = count;
	}

	public bool All<TOperator>() where TOperator : IFunction<int, bool>
	{
		for (var i = _start; i < _start + _count; i++)
		{
			if (!TOperator.Eval(i))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		return _count != 0;
	}

	public IEnumerable<int> AsEnumerable()
	{
		return new QueryAsEnumerable<int, RangeQuery, RangeEnumerator>(this);
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

	public int ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			return _start + Int32.CreateChecked(index);
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public int ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			return _start + Int32.CreateChecked(index);
		}

		return default;
	}

	public int First()
	{
		return _start;
	}

	public int FirstOrDefault()
	{
		return _start;
	}

	public int Last()
	{
		if (_count <= 0)
		{
			throw new Exception("Sequence doesn't contain elements");
		}

		return _start + _count;
	}

	public int LastOrDefault()
	{
		if (_count <= 0)
		{
			return default;
		}

		return _start + _count;
	}

	public int Max()
	{
		return _start + _count;
	}

	public int Min()
	{
		return _start;
	}

	public int Single()
	{
		if (_count == 1)
		{
			return _start;
		}

		throw new Exception("Sequence contains contains to much elements");
	}

	public int SingleOrDefault()
	{
		if (_count == 1)
		{
			return _start;
		}

		return default;
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

	public List<int> ToList()
	{
		var list = new List<int>(_count);
		
		CopyTo(CollectionsMarshal.AsSpan(list));

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = _count;
		return true;
	}

	public WhereQuery<int, TOperator, RangeQuery, RangeEnumerator> Where<TOperator>() where TOperator : IFunction<int, bool>
	{
		return new WhereQuery<int, TOperator, RangeQuery, RangeEnumerator>(this);
	}

	public SelectQuery<int, TResult, TOperator, RangeQuery, RangeEnumerator> Select<TOperator, TResult>() where TOperator : IFunction<int, TResult>
	{
		return new SelectQuery<int, TResult, TOperator, RangeQuery, RangeEnumerator>(this);
	}

	public SelectQuery<int, int, TOperator, RangeQuery, RangeEnumerator> Select<TOperator>() where TOperator : IFunction<int, int>
	{
		return new SelectQuery<int, int, TOperator, RangeQuery, RangeEnumerator>(this);
	}

	public SkipQuery<TCount, int, RangeQuery, RangeEnumerator> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, int, RangeQuery, RangeEnumerator>(this, count);
	}

	public TakeQuery<TCount, int, RangeQuery, RangeEnumerator> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, int, RangeQuery, RangeEnumerator>(this, count);
	}

	public RangeEnumerator GetEnumerator()
	{
		return new RangeEnumerator(_start, _count);
	}
}