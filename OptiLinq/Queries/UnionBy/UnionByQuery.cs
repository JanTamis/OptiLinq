using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer> : IOptiQuery<T, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>>
	where TFirstQuery : struct, IOptiQuery<T, TFirstEnumerator>
	where TSecondQuery : struct, IOptiQuery<T>
	where TFirstEnumerator : IEnumerator<T>
	where TKeySelector : struct, IFunction<T, TKey>
	where TComparer : IEqualityComparer<TKey>
{
	private TFirstQuery _firstQuery;
	private TSecondQuery _secondQuery;

	private readonly TComparer _comparer;
	private TKeySelector _keySelector;

	private int _count = -1;

	internal UnionByQuery(ref TFirstQuery firstQuery, ref TSecondQuery secondQuery, TKeySelector selector, TComparer comparer)
	{
		_firstQuery = firstQuery;
		_secondQuery = secondQuery;

		_comparer = comparer;
		_keySelector = selector;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				seed = func.Eval(in seed, firstEnumerator.Current);
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				seed = func.Eval(in seed, secondEnumerator.Current);
			}
		}

		_count = set.Count;
		return selector.Eval(in seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				seed = @operator.Eval(in seed, firstEnumerator.Current);
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				seed = @operator.Eval(in seed, secondEnumerator.Current);
			}
		}

		_count = set.Count;
		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)) && !@operator.Eval(firstEnumerator.Current))
			{
				return false;
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)) && !@operator.Eval(secondEnumerator.Current))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		return _firstQuery.Any() || _secondQuery.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)) && @operator.Eval(firstEnumerator.Current))
			{
				return true;
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)) && @operator.Eval(secondEnumerator.Current))
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

	public bool Contains<TComparer1>(in T item, TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)) && comparer.Equals(firstEnumerator.Current, item))
			{
				return true;
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)) && comparer.Equals(secondEnumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(in T item)
	{
		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)) && EqualityComparer<T>.Default.Equals(firstEnumerator.Current, item))
			{
				return true;
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)) && EqualityComparer<T>.Default.Equals(secondEnumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		var index = 0;

		while (firstEnumerator.MoveNext() && index < data.Length)
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				data[index++] = firstEnumerator.Current;
			}
		}

		while (secondEnumerator.MoveNext() && index < data.Length)
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				data[index++] = secondEnumerator.Current;
			}
		}

		return index;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		if (TryGetNonEnumeratedCount(out var count))
		{
			return TNumber.CreateChecked(count);
		}

		var result = TNumber.Zero;

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				result++;
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				result++;
			}
		}

		_count = set.Count;

		return result;
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
		var result = TNumber.Zero;

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)) && @operator.Eval(firstEnumerator.Current))
			{
				result++;
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)) && @operator.Eval(secondEnumerator.Current))
			{
				result++;
			}
		}

		return result;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var result = TNumber.Zero;

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)) && predicate(firstEnumerator.Current))
			{
				result++;
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)) && predicate(secondEnumerator.Current))
			{
				result++;
			}
		}

		return result;
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
		return EnumerableHelper.TryGetElementAt(GetEnumerator(), index, out item);
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
		return _firstQuery.TryGetFirst(out item) || _secondQuery.TryGetFirst(out item);
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
		if (TryGetNonEnumeratedCount(out _count) && _count == 0)
		{
			return Task.CompletedTask;
		}

		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				Task.Factory.StartNew(x => @operator.Do((T)x), firstEnumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				Task.Factory.StartNew(x => @operator.Do((T)x), secondEnumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
			}
		}

		_count = set.Count;

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		if (TryGetNonEnumeratedCount(out _count) && _count == 0)
		{
			return;
		}

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				@operator.Do(firstEnumerator.Current);
				_count++;
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				@operator.Do(secondEnumerator.Current);
				_count++;
			}
		}

		_count = set.Count;
	}

	public bool TryGetLast(out T item)
	{
		return _secondQuery.TryGetLast(out item) || _firstQuery.TryGetLast(out item);
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
		return EnumerableHelper.Max<T, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		using var enumerator = GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;

			if (!enumerator.MoveNext())
			{
				return true;
			}
		}

		item = default;
		return false;
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
		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var builder = new PooledList<T>();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				builder.Add(firstEnumerator.Current);
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				builder.Add(secondEnumerator.Current);
			}
		}

		_count = builder.Count;
		return builder.ToArray();
	}

	public T[] ToArray(out int length)
	{
		var array = ToArray();
		length = array.Length;
		return array;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var set = TryGetNonEnumeratedCount(out var count)
			? new HashSet<T>(count, comparer)
			: new HashSet<T>(comparer);

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public List<T> ToList()
	{
		var list = TryGetNonEnumeratedCount(out var count)
			? new List<T>(count)
			: new List<T>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = TryGetNonEnumeratedCount(out var count)
			? new PooledList<T>(count)
			: new PooledList<T>();

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				list.Add(firstEnumerator.Current);
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				list.Add(secondEnumerator.Current);
			}
		}

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = TryGetNonEnumeratedCount(out var count)
			? new PooledQueue<T>(count)
			: new PooledQueue<T>();

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				queue.Enqueue(firstEnumerator.Current);
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				queue.Enqueue(secondEnumerator.Current);
			}
		}

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = TryGetNonEnumeratedCount(out var count)
			? new PooledStack<T>(count)
			: new PooledStack<T>();

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				stack.Push(firstEnumerator.Current);
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				stack.Push(secondEnumerator.Current);
			}
		}

		return stack;
	}

	public PooledSet<T, TComparer1> ToPooledSet<TComparer1>(TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		var resultSet = TryGetNonEnumeratedCount(out var count)
			? new PooledSet<T, TComparer1>(count, comparer)
			: new PooledSet<T, TComparer1>(comparer);

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();
		using var set = GetSet();

		while (firstEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(firstEnumerator.Current)))
			{
				resultSet.Add(firstEnumerator.Current);
			}
		}

		while (secondEnumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(secondEnumerator.Current)))
			{
				resultSet.Add(secondEnumerator.Current);
			}
		}

		return resultSet;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_count != -1)
		{
			length = _count;
			return true;
		}

		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer> GetEnumerator()
	{
		return new UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>(_firstQuery.GetEnumerator(), _secondQuery.GetEnumerator(), _keySelector, GetSet());
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private PooledSet<TKey, TComparer> GetSet()
	{
		if (TryGetNonEnumeratedCount(out var count))
		{
			return new PooledSet<TKey, TComparer>(count, _comparer);
		}

		return new PooledSet<TKey, TComparer>(_comparer);
	}
}