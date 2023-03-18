using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct HashSetQuery<T> : IOptiQuery<T, HashSet<T>.Enumerator>
{
	private readonly HashSet<T> _set;

	internal HashSetQuery(HashSet<T> set)
	{
		_set = set;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			seed = func.Eval(seed, enumerator.Current);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			seed = @operator.Eval(seed, enumerator.Current);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (!@operator.Eval(enumerator.Current))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		return _set.Count != 0;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (@operator.Eval(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return _set;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (comparer.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(in T item)
	{
		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (EqualityComparer<T>.Default.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		using var enumerator = _set.GetEnumerator();
		var i = 0;

		while (enumerator.MoveNext())
		{
			data[i++] = enumerator.Current;
		}

		return i;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return TNumber.CreateChecked(_set.Count);
	}

	public int Count()
	{
		return _set.Count;
	}

	public long LongCount()
	{
		return _set.Count;
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T, bool>
	{
		var count = TNumber.Zero;

		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (@operator.Eval(enumerator.Current))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (predicate(enumerator.Current))
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
		if (index >= TIndex.Zero)
		{
			using var enumerator = _set.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (index == TIndex.Zero)
				{
					item = enumerator.Current;
					return true;
				}

				index--;
			}
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
		using var enumerator = _set.GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;
			return true;
		}

		item = default!;
		return false;
	}

	public T First()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence was empty");
	}

	public T FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			Task.Factory.StartNew(x => @operator.Do(x is null ? default : (T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			@operator.Do(enumerator.Current);
		}
	}

	public bool TryGetLast(out T item)
	{
		using var enumerator = _set.GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;

			while (enumerator.MoveNext())
			{
				item = enumerator.Current;
			}

			return true;
		}

		item = default!;
		return false;
	}

	public T Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence was empty");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, HashSet<T>.Enumerator>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, HashSet<T>.Enumerator>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		using var enumerator = _set.GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;

			if (!enumerator.MoveNext())
			{
				return true;
			}
		}

		item = default!;
		return false;
	}

	public T Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence was empty or contained more than one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		var array = new T[_set.Count];

		_set.CopyTo(array, 0);

		return array;
	}

	public T[] ToArray(out int length)
	{
		length = _set.Count;
		return ToArray();
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		return new HashSet<T>(_set, comparer);
	}

	public List<T> ToList()
	{
		var list = new List<T>(_set.Count);
		var span = CollectionsMarshal.AsSpan(list);
		using var enumerator = _set.GetEnumerator();

		for (var i = 0; i < span.Length && enumerator.MoveNext(); i++)
		{
			span[i] = enumerator.Current;
		}

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = new PooledList<T>(_set.Count)
		{
			Count = _set.Count,
		};

		_set.CopyTo(list.Items);

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = new PooledQueue<T>(_set.Count);

		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			queue.Enqueue(enumerator.Current);
		}

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = new PooledStack<T>(_set.Count);

		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			stack.Push(enumerator.Current);
		}

		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		var set = new PooledSet<T, TComparer>(_set.Count, comparer);

		using var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = _set.Count;
		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public HashSet<T>.Enumerator GetEnumerator()
	{
		return _set.GetEnumerator();
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}