using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, SkipEnumerator<TCount, T, TBaseEnumerator>>
	where TBaseEnumerator : IEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TCount : IBinaryInteger<TCount>
{
	private TBaseQuery _baseEnumerable;
	private readonly TCount _count;

	internal SkipQuery(ref TBaseQuery baseEnumerable, TCount count)
	{
		_baseEnumerable = baseEnumerable;
		_count = count;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			seed = func.Eval(seed, enumerator.Current);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			seed = @operator.Eval(seed, enumerator.Current);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

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
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		return enumerator.MoveNext();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

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
		return this;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			if (comparer.Equals(item, enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(in T item)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			if (EqualityComparer<T>.Default.Equals(item, enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var j = TCount.Zero; j < _count && enumerator.MoveNext(); j++)
		{
		}

		var i = 0;

		for (; i < data.Length && enumerator.MoveNext(); i++)
		{
			data[i] = enumerator.Current;
		}

		return i;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		var count = _baseEnumerable.Count<TNumber>() - TNumber.CreateChecked(_count);

		if (TNumber.IsNegative(count))
		{
			return TNumber.Zero;
		}

		return count;
	}

	public int Count()
	{
		return Count<int>();
	}

	public long LongCount()
	{
		return Count<long>();
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T, bool>
	{
		var count = TNumber.Zero;

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

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

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

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
		if (TIndex.IsPositive(index))
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
			{
			}

			while (enumerator.MoveNext())
			{
				if (TIndex.IsZero(index))
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

		throw new ArgumentOutOfRangeException(nameof(index));
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out T item)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

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

		throw new InvalidOperationException("The sequence is empty");
	}

	public T FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			@operator.Do(enumerator.Current);
		}
	}

	public bool TryGetLast(out T item)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();
		var isValid = false;

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		if (enumerator.MoveNext())
		{
			isValid = true;
		}

		if (!isValid)
		{
			item = default!;
			return false;
		}

		var value = enumerator.Current;

		while (enumerator.MoveNext())
		{
			value = enumerator.Current;
		}

		item = value;
		return true;
	}

	public T Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence is empty");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, SkipEnumerator<TCount, T, TBaseEnumerator>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, SkipEnumerator<TCount, T, TBaseEnumerator>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		item = default!;

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;
		}

		if (enumerator.MoveNext())
		{
			item = default!;
			return false;
		}

		return true;
	}

	public T Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence does not contain exactly one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		LargeArrayBuilder<T> builder = new();

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			builder.Add(enumerator.Current);
		}

		return builder.ToArray();
	}

	public T[] ToArray(out int length)
	{
		return EnumerableHelper.ToArray<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(this, out length);
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var set = _baseEnumerable.TryGetNonEnumeratedCount(out var count)
			? new HashSet<T>(Math.Max(count - Int32.CreateTruncating(_count), 0), comparer)
			: new HashSet<T>(comparer);

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public List<T> ToList()
	{
		var list = new List<T>();
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = new PooledList<T>();
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = new PooledQueue<T>();
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			queue.Enqueue(enumerator.Current);
		}

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = new PooledStack<T>();
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			stack.Push(enumerator.Current);
		}

		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		var set = new PooledSet<T, TComparer>(comparer);
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		if (_baseEnumerable.TryGetSpan(out span))
		{
			var count = Math.Min(span.Length, Int32.CreateTruncating(_count));

			span = span.Slice(count);
			return true;
		}

		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public SkipEnumerator<TCount, T, TBaseEnumerator> GetEnumerator()
	{
		return new SkipEnumerator<TCount, T, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _count);
	}

	IEnumerator<T> IEnumerable<T>.
		GetEnumerator() => GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}