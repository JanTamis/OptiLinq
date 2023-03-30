using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ListQuery<T> : IOptiQuery<T, List<T>.Enumerator>
{
	private readonly List<T> _list;

	internal ListQuery(List<T> list)
	{
		_list = list;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		foreach (var item in CollectionsMarshal.AsSpan(_list))
		{
			seed = func.Eval(in seed, in item);
		}

		return selector.Eval(in seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		foreach (var item in CollectionsMarshal.AsSpan(_list))
		{
			seed = @operator.Eval(in seed, in item);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		foreach (var item in CollectionsMarshal.AsSpan(_list))
		{
			if (!@operator.Eval(in item))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		return _list.Count != 0;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		foreach (var item in CollectionsMarshal.AsSpan(_list))
		{
			if (@operator.Eval(in item))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return _list;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		foreach (var currentItem in CollectionsMarshal.AsSpan(_list))
		{
			if (comparer.Equals(currentItem, item))
			{
				return false;
			}
		}

		return true;
	}

	public bool Contains(in T item)
	{
		foreach (var currentItem in CollectionsMarshal.AsSpan(_list))
		{
			if (EqualityComparer<T>.Default.Equals(currentItem, item))
			{
				return false;
			}
		}

		return true;
	}

	public int CopyTo(Span<T> data)
	{
		var length = Math.Min(_list.Count, data.Length);

		CollectionsMarshal.AsSpan(_list).Slice(0, length).CopyTo(data);

		return length;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return TNumber.CreateChecked(_list.Count);
	}

	public int Count()
	{
		return _list.Count;
	}

	public long LongCount()
	{
		return _list.Count;
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T, bool>
	{
		var count = TNumber.Zero;

		foreach (var currentItem in CollectionsMarshal.AsSpan(_list))
		{
			if (@operator.Eval(in currentItem))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		foreach (var currentItem in CollectionsMarshal.AsSpan(_list))
		{
			if (predicate(currentItem))
			{
				count++;
			}
		}

		return count;
	}

	public int Count(Func<T, bool> predicate)
	{
		return Count<int>(predicate);
	}

	public long CountLong(Func<T, bool> predicate)
	{
		return Count<long>(predicate);
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		if (TIndex.IsPositive(index))
		{
			item = _list[Int32.CreateChecked(index)];
			return true;
		}

		item = default!;
		return false;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out T item)
	{
		if (_list.Count == 0)
		{
			item = default!;
			return false;
		}

		item = _list[0];
		return true;
	}

	public T First()
	{
		if (_list.Count == 0)
		{
			throw new Exception("Sequence doesn't contain a element");
		}

		return _list[0];
	}

	public T FirstOrDefault()
	{
		if (_list.Count == 0)
		{
			return default;
		}

		return _list[0];
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		foreach (var item in CollectionsMarshal.AsSpan(_list))
		{
			Task.Factory.StartNew(x => @operator.Do(x is null ? default : (T)x), item, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		foreach (var item in CollectionsMarshal.AsSpan(_list))
		{
			@operator.Do(in item);
		}
	}

	public bool TryGetLast(out T item)
	{
		if (_list.Count == 0)
		{
			item = default!;
			return false;
		}

		item = _list[^1];
		return true;
	}

	public T Last()
	{
		if (_list.Count == 0)
		{
			throw new Exception("Sequence doesn't contain a element");
		}

		return _list[^1];
	}

	public T LastOrDefault()
	{
		if (_list.Count == 0)
		{
			return default;
		}

		return _list[^1];
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, List<T>.Enumerator>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, List<T>.Enumerator>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		if (_list.Count != 1)
		{
			item = default!;
			return false;
		}

		item = _list[0];
		return true;
	}

	public T Single()
	{
		if (_list.Count != 1)
		{
			throw new Exception("Sequence contains to much elements");
		}

		return _list[0];
	}

	public T SingleOrDefault()
	{
		if (_list.Count != 1)
		{
			return default;
		}

		return _list[0];
	}

	public T[] ToArray()
	{
		var array = new T[_list.Count];

		_list.CopyTo(array, 0);

		return array;
	}

	public T[] ToArray(out int length)
	{
		length = _list.Count;
		return ToArray();
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var set = new HashSet<T>(_list.Count, comparer);

		foreach (var item in CollectionsMarshal.AsSpan(_list))
		{
			set.Add(item);
		}

		return set;
	}

	public List<T> ToList()
	{
		var list = new List<T>(_list.Count);
		var span = CollectionsMarshal.AsSpan(list);

		for (var i = 0; i < span.Length; i++)
		{
			span[i] = _list[i];
		}

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = new PooledList<T>(_list.Count)
		{
			Count = _list.Count,
		};

		var span = CollectionsMarshal.AsSpan(_list);
		span.CopyTo(list.Items);

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = new PooledQueue<T>(_list.Count);

		foreach (var item in CollectionsMarshal.AsSpan(_list))
		{
			queue.Enqueue(item);
		}

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = new PooledStack<T>(_list.Count);

		foreach (var item in CollectionsMarshal.AsSpan(_list))
		{
			stack.Push(item);
		}

		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		var set = new PooledSet<T, TComparer>(comparer);

		foreach (var item in CollectionsMarshal.AsSpan(_list))
		{
			set.Add(item);
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = _list.Count;
		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = CollectionsMarshal.AsSpan(_list);
		return true;
	}

	public List<T>.Enumerator GetEnumerator()
	{
		return _list.GetEnumerator();
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}