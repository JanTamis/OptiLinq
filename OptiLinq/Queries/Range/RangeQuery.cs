using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RangeQuery : IOptiQuery<int, RangeEnumerator>
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

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, int, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		for (var i = _start; i < _start + _count; i++)
		{
			seed = func.Eval(seed, i);
		}

		return selector.Eval(seed);
	}

	public readonly TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, int, TAccumulate>
	{
		for (var i = _start; i < _start + _count; i++)
		{
			seed = @operator.Eval(seed, i);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<int, bool>
	{
		for (var i = _start; i < _start + _count; i++)
		{
			if (!@operator.Eval(i))
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

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<int, bool>
	{
		for (var i = _start; i < _start + _count; i++)
		{
			if (@operator.Eval(i))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<int> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in int item, TComparer comparer) where TComparer : IEqualityComparer<int>
	{
		for (var i = _start; i < _start + _count; i++)
		{
			if (comparer.Equals(item, i))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(in int item)
	{
		return item >= _start && item <= _count + _start;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return TNumber.CreateChecked(_count);
	}

	public int Count()
	{
		return Count<int>();
	}

	public long LongCount()
	{
		return Count<long>();
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<int, bool>
	{
		var count = TNumber.Zero;

		for (var i = _start; i < _start + _count; i++)
		{
			if (@operator.Eval(i))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<int, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		for (var i = _start; i < _start + _count; i++)
		{
			if (predicate(i))
			{
				count++;
			}
		}

		return count;
	}

	public int Count(Func<int, bool> predicate)
	{
		return Count<int>(predicate);
	}

	public long CountLong(Func<int, bool> predicate)
	{
		return Count<long>(predicate);
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out int item) where TIndex : IBinaryInteger<TIndex>
	{
		if (TIndex.IsPositive(index) && index < TIndex.CreateSaturating(_count))
		{
			item = _start + Int32.CreateChecked(index);
			return true;
		}

		item = default;
		return false;
	}

	public int ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public int ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out int item)
	{
		if (_count > 0)
		{
			item = _start;
			return true;
		}

		item = default;
		return false;
	}

	public int First()
	{
		if (_count <= 0)
		{
			throw new Exception("Sequence doesn't contain elements");
		}

		return _start;
	}

	public int FirstOrDefault()
	{
		if (_count <= 0)
		{
			return default;
		}

		return _start;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<int>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		var options = new ParallelOptions
		{
			CancellationToken = token,
			TaskScheduler = schedulerPair.ConcurrentScheduler,
		};

		Parallel.For(_start, _start + _count, options, x => @operator.Do(x));

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<int>
	{
		for (var i = _start; i < _start + _count; i++)
		{
			@operator.Do(i);
		}
	}

	public bool TryGetLast(out int item)
	{
		if (_count <= 0)
		{
			item = default;
			return false;
		}

		item = _start + _count;
		return true;
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

	public bool TryGetSingle(out int item)
	{
		if (_count == 1)
		{
			item = _start;
			return true;
		}

		item = default;
		return false;
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

	private readonly int[] _buffer = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int CopyTo(Span<int> data)
	{
		var count = Math.Min(_count, data.Length);
		var start = _start;

		ref var first = ref MemoryMarshal.GetReference(data);

		if (Vector.IsHardwareAccelerated && count >= Vector<int>.Count)
		{
			var increment = new Vector<int>(Vector<int>.Count);
			var vector = new Vector<int>(_buffer);

			if (_start > 0)
			{
				vector += new Vector<int>(_start);
			}

			do
			{
				Unsafe.As<int, Vector<int>>(ref first) = vector;
				vector += increment;

				first = ref Unsafe.Add(ref first, Vector<int>.Count);
				count -= Vector<int>.Count;
			} while (count >= Vector<int>.Count);

			start = Unsafe.Subtract(ref first, 1);
		}

		for (var i = 0; i < count; i++)
			Unsafe.Add(ref first, i) = ++start;

		return count;
	}

	public int[] ToArray()
	{
		var array = GC.AllocateUninitializedArray<int>(_count);

		CopyTo(array);

		return array;
	}

	public int[] ToArray(out int length)
	{
		length = _count;
		return ToArray();
	}

	public HashSet<int> ToHashSet(IEqualityComparer<int>? comparer = default)
	{
		var set = new HashSet<int>(_count, comparer);

		for (var i = _start; i < _start + _count; i++)
		{
			set.Add(i);
		}

		return set;
	}

	public List<int> ToList()
	{
		var list = new List<int>(_count);

		CopyTo(CollectionsMarshal.AsSpan(list));

		return list;
	}

	public PooledList<int> ToPooledList()
	{
		var list = new PooledList<int>(_count)
		{
			Count = _count,
		};

		CopyTo(list.Items);

		return list;
	}

	public PooledQueue<int> ToPooledQueue()
	{
		var queue = new PooledQueue<int>(_count)
		{
			Count = _count,
		};

		CopyTo(queue._array);

		return queue;
	}

	public PooledStack<int> ToPooledStack()
	{
		var stack = new PooledStack<int>(_count)
		{
			Count = _count,
		};

		CopyTo(stack._array);

		return stack;
	}

	public PooledSet<int, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<int>
	{
		var set = new PooledSet<int, TComparer>(_count, comparer);

		for (var i = _start; i < _start + _count; i++)
		{
			set.Add(i);
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = _count;
		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<int> span)
	{
		span = ReadOnlySpan<int>.Empty;
		return false;
	}

	public double Average()
	{
		return (_start + _count) / 2.0;
	}

	public int Sum()
	{
		return _count * (_start + _start + _count) >> 1;
	}

	public RangeEnumerator GetEnumerator()
	{
		return new RangeEnumerator(_start, _count);
	}

	IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}