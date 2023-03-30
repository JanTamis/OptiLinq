using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery> : IOptiQuery<T, ConcatEnumerator<T, TFirstEnumerator, IEnumerator<T>>>
	where TFirstQuery : struct, IOptiQuery<T, TFirstEnumerator>
	where TSecondQuery : struct, IOptiQuery<T>
	where TFirstEnumerator : IEnumerator<T>
{
	private TFirstQuery _firstQuery;
	private TSecondQuery _secondQuery;

	internal ConcatQuery(ref TFirstQuery firstQuery, ref TSecondQuery secondQuery)
	{
		_firstQuery = firstQuery;
		_secondQuery = secondQuery;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		return _secondQuery.Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(_firstQuery.Aggregate(seed, func), func, selector);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		return _secondQuery.Aggregate(_firstQuery.Aggregate(seed, @operator), @operator);
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		return _firstQuery.All(@operator) && _secondQuery.All(@operator);
	}

	public bool Any()
	{
		return _firstQuery.Any() || _secondQuery.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		return _firstQuery.Any(@operator) || _secondQuery.Any(@operator);
	}

	public IEnumerable<T> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return _firstQuery.Contains(in item, comparer) || _secondQuery.Contains(in item, comparer);
	}

	public bool Contains(in T item)
	{
		return _firstQuery.Contains(in item) || _secondQuery.Contains(in item);
	}

	public int CopyTo(Span<T> data)
	{
		var firstCount = _firstQuery.CopyTo(data);
		return firstCount + _secondQuery.CopyTo(data.Slice(firstCount));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return _firstQuery.Count<TNumber>() + _secondQuery.Count<TNumber>();
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
		return _firstQuery.Count<TCountOperator, TNumber>(@operator) + _secondQuery.Count<TCountOperator, TNumber>(@operator);
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		return _firstQuery.Count<TNumber>(predicate) + _secondQuery.Count<TNumber>(predicate);
	}

	public int Count(Func<T, bool> predicate)
	{
		return _firstQuery.Count(predicate) + _secondQuery.Count(predicate);
	}

	public long CountLong(Func<T, bool> predicate)
	{
		return _firstQuery.CountLong(predicate) + _secondQuery.CountLong(predicate);
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

		throw new IndexOutOfRangeException("Index was out of bounds");
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

		throw new InvalidOperationException("Sequence was empty");
	}

	public T FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		return Task.WhenAll(_firstQuery.ForAll(@operator, token), _secondQuery.ForAll(@operator, token));
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		_firstQuery.ForEach(@operator);
		_secondQuery.ForEach(@operator);
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

		throw new InvalidOperationException("Sequence was empty");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		var firstMax = _firstQuery.Max();
		var secondMax = _secondQuery.Max();

		return Comparer<T>.Default.Compare(firstMax, secondMax) > 0
			? firstMax
			: secondMax;
	}

	public T Min()
	{
		var firstMin = _firstQuery.Min();
		var secondMin = _secondQuery.Min();

		return Comparer<T>.Default.Compare(firstMin, secondMin) < 0
			? firstMin
			: secondMin;
	}

	public bool TryGetSingle(out T item)
	{
		using var enumerator = GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;

			return !enumerator.MoveNext();
		}

		item = default!;
		return false;
	}

	public T Single()
	{
		if (TryGetFirst(out var result))
		{
			return result;
		}

		throw new InvalidOperationException("Sequence does not contain exactly one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var result);
		return result;
	}

	public T[] ToArray()
	{
		if (_firstQuery.TryGetNonEnumeratedCount(out var firstCount) && _secondQuery.TryGetNonEnumeratedCount(out var secondCount))
		{
			var array = new T[firstCount + secondCount];

			_firstQuery.CopyTo(array);
			_secondQuery.CopyTo(array.AsSpan(firstCount));

			return array;
		}

		using var builder = _firstQuery.ToPooledList();

		if (_secondQuery.TryGetNonEnumeratedCount(out var count))
		{
			builder.EnsureCapacity(builder.Count + count);
			_secondQuery.CopyTo(builder.Items.AsSpan(builder.Count - count));
		}
		else
		{
			using var enumerator = _secondQuery.GetEnumerator();

			while (enumerator.MoveNext())
			{
				builder.Add(enumerator.Current);
			}
		}

		return builder.ToArray();
	}
	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		using var firstEnumerator = _firstQuery.GetEnumerator();
		var set = _secondQuery.ToHashSet(comparer);

		while (firstEnumerator.MoveNext())
		{
			set.Add(firstEnumerator.Current);
		}
		
		return set;
	}

	public List<T> ToList()
	{
		var firstCount = 0;
		var secondCount = 0;

		List<T> list;

		if (_firstQuery.TryGetNonEnumeratedCount(out firstCount) && _secondQuery.TryGetNonEnumeratedCount(out secondCount))
		{
			list = new List<T>(firstCount + secondCount);
			var span = CollectionsMarshal.AsSpan(list);

			_firstQuery.CopyTo(span);
			_secondQuery.CopyTo(span.Slice(firstCount));
		}
		else
		{
			list = new List<T>(firstCount + secondCount);

			using var firstEnumerator = _firstQuery.GetEnumerator();
			using var secondEnumerator = _secondQuery.GetEnumerator();

			while (firstEnumerator.MoveNext())
			{
				list.Add(firstEnumerator.Current);
			}

			while (secondEnumerator.MoveNext())
			{
				list.Add(secondEnumerator.Current);
			}
		}

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = new PooledList<T>();

		if (_firstQuery.TryGetNonEnumeratedCount(out var count))
		{
			list.EnsureCapacity(list.Count + count);
			_firstQuery.CopyTo(list.Items.AsSpan(list.Count));
		}
		else
		{
			using var enumerator = _firstQuery.GetEnumerator();

			while (enumerator.MoveNext())
			{
				list.Add(enumerator.Current);
			}
		}

		if (_secondQuery.TryGetNonEnumeratedCount(out count))
		{
			list.EnsureCapacity(list.Count + count);
			_secondQuery.CopyTo(list.Items.AsSpan(list.Count));
		}
		else
		{
			using var enumerator = _secondQuery.GetEnumerator();

			while (enumerator.MoveNext())
			{
				list.Add(enumerator.Current);
			}
		}

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = new PooledQueue<T>();

		if (_firstQuery.TryGetNonEnumeratedCount(out var count))
		{
			queue.EnsureCapacity(queue.Count + count);
			_firstQuery.CopyTo(queue._array.AsSpan(queue.Count));
		}
		else
		{
			using var enumerator = _firstQuery.GetEnumerator();

			while (enumerator.MoveNext())
			{
				queue.Enqueue(enumerator.Current);
			}
		}

		if (_secondQuery.TryGetNonEnumeratedCount(out count))
		{
			queue.EnsureCapacity(queue.Count + count);
			_secondQuery.CopyTo(queue._array.AsSpan(queue.Count));
		}
		else
		{
			using var enumerator = _secondQuery.GetEnumerator();

			while (enumerator.MoveNext())
			{
				queue.Enqueue(enumerator.Current);
			}
		}

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = new PooledStack<T>();

		if (_firstQuery.TryGetNonEnumeratedCount(out var count))
		{
			stack.EnsureCapacity(stack.Count + count);
			_firstQuery.CopyTo(stack._array.AsSpan(stack.Count));
		}
		else
		{
			using var enumerator = _firstQuery.GetEnumerator();

			while (enumerator.MoveNext())
			{
				stack.Push(enumerator.Current);
			}
		}

		if (_secondQuery.TryGetNonEnumeratedCount(out count))
		{
			stack.EnsureCapacity(stack.Count + count);
			_secondQuery.CopyTo(stack._array.AsSpan(stack.Count));
		}
		else
		{
			using var enumerator = _secondQuery.GetEnumerator();

			while (enumerator.MoveNext())
			{
				stack.Push(enumerator.Current);
			}
		}

		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		var set = _secondQuery.ToPooledSet(comparer);

		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_firstQuery.TryGetNonEnumeratedCount(out var firstCount) && _secondQuery.TryGetNonEnumeratedCount(out var secondCount))
		{
			length = firstCount + secondCount;
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

	public ConcatEnumerator<T, TFirstEnumerator, IEnumerator<T>> GetEnumerator()
	{
		return new ConcatEnumerator<T, TFirstEnumerator, IEnumerator<T>>(_firstQuery.GetEnumerator(), _secondQuery.GetEnumerator());
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}