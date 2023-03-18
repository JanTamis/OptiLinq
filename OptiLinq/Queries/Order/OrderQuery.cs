using System.Buffers;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer> : IOptiQuery<T, OrderEnumerator<T, TComparer, TBaseEnumerator>>
	where TBaseEnumerator : IEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TComparer : IComparer<T>
{
	private TBaseQuery _baseEnumerable;
	private readonly TComparer _comparer;

	private int _count = -1;

	internal OrderQuery(ref TBaseQuery baseEnumerable, TComparer comparer)
	{
		_baseEnumerable = baseEnumerable;
		_comparer = comparer;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		return _baseEnumerable.Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(func, selector, seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		return _baseEnumerable.Aggregate(@operator, seed);
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		return _baseEnumerable.All(@operator);
	}

	public bool Any()
	{
		return _baseEnumerable.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		return _baseEnumerable.Any(@operator);
	}

	public IEnumerable<T> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer1>(in T item, TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		return _baseEnumerable.Contains(in item, comparer);
	}

	public bool Contains(in T item)
	{
		return _baseEnumerable.Contains(in item);
	}

	public int CopyTo(Span<T> data)
	{
		var length = _baseEnumerable.CopyTo(data);

		data.Slice(0, length).Sort(_comparer);

		return length;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return _baseEnumerable.Count<TNumber>();
	}

	public int Count()
	{
		return _baseEnumerable.Count();
	}

	public long LongCount()
	{
		return _baseEnumerable.LongCount();
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T, bool>
	{
		return _baseEnumerable.Count<TCountOperator, TNumber>(@operator);
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		return _baseEnumerable.Count<TNumber>(predicate);
	}

	public int Count(Func<T, bool> predicate)
	{
		return _baseEnumerable.Count(predicate);
	}

	public long CountLong(Func<T, bool> predicate)
	{
		return _baseEnumerable.CountLong(predicate);
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		if (TIndex.IsPositive(index))
		{
			TryGetNonEnumeratedCount(out var count);

			using var enumerator = GetEnumerator();
			using var list = new PooledList<T>(Math.Max(count, 4));

			while (enumerator.MoveNext())
			{
				list.Add(enumerator.Current);
			}

			list.Items.AsSpan(0, list.Count).Sort(_comparer);

			var indexInt = Int32.CreateChecked(index);

			if (indexInt < list.Count)
			{
				item = list.Items[indexInt];
				return true;
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
		using var enumerator = GetEnumerator();

		if (!enumerator.MoveNext())
		{
			item = default!;
			return false;
		}

		item = enumerator.Current;

		while (enumerator.MoveNext())
		{
			if (Comparer<T>.Default.Compare(item, enumerator.Current) < 0)
			{
				item = enumerator.Current;
			}
		}

		return true;
	}

	public T First()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence contains no elements");
	}

	public T FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		return _baseEnumerable.ForAll(@operator, token);
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		_baseEnumerable.TryGetNonEnumeratedCount(out _count);
		var data = EnumerableHelper.ToArray(_baseEnumerable.GetEnumerator(), ArrayPool<T>.Shared, Math.Max(4, _count), out _count);

		data.AsSpan(0, _count).Sort(_comparer);

		for (var i = 0; i < _count; i++)
		{
			@operator.Do(data[i]);
		}

		ArrayPool<T>.Shared.Return(data, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
	}

	public bool TryGetLast(out T item)
	{
		using var enumerator = GetEnumerator();

		if (!enumerator.MoveNext())
		{
			item = default!;
			return false;
		}

		item = enumerator.Current;

		while (enumerator.MoveNext())
		{
			if (Comparer<T>.Default.Compare(item, enumerator.Current) > 0)
			{
				item = enumerator.Current;
			}
		}

		return true;
	}

	public T Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence contains no elements");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return _baseEnumerable.Max();
	}

	public T Min()
	{
		return _baseEnumerable.Min();
	}

	public bool TryGetSingle(out T item)
	{
		TryGetNonEnumeratedCount(out var count);

		using var enumerator = GetEnumerator();
		using var list = new PooledList<T>(Math.Max(count, 4));

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		list.Items.AsSpan(0, list.Count).Sort(_comparer);

		if (list.Count == 1)
		{
			item = list.Items[0];
			return true;
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

		throw new InvalidOperationException("Sequence does not contain exactly one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		var array = _baseEnumerable.ToArray();

		array.AsSpan().Sort(_comparer);

		return array;
	}

	public T[] ToArray(out int length)
	{
		var array = _baseEnumerable.ToArray(out length);

		array.AsSpan(0, length).Sort(_comparer);

		return array;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		return _baseEnumerable.ToHashSet(comparer);
	}

	public List<T> ToList()
	{
		var list = _baseEnumerable.ToList();

		CollectionsMarshal.AsSpan(list).Sort(_comparer);

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = _baseEnumerable.ToPooledList();

		list.Items.AsSpan(0, list.Count).Sort(_comparer);

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		using var tempList = _baseEnumerable.ToPooledList();

		tempList.Items.AsSpan(0, tempList.Count).Sort(_comparer);

		var result = new PooledQueue<T>(tempList.Count);

		for (var i = 0; i < tempList.Count; i++)
		{
			result.Enqueue(tempList.Items[i]);
		}

		return result;
	}

	public PooledStack<T> ToPooledStack()
	{
		using var tempList = _baseEnumerable.ToPooledList();

		tempList.Items.AsSpan(0, tempList.Count).Sort(_comparer);

		var result = new PooledStack<T>(tempList.Count);

		for (var i = 0; i < tempList.Count; i++)
		{
			result.Push(tempList.Items[i]);
		}

		return result;
	}

	public PooledSet<T, TComparer1> ToPooledSet<TComparer1>(TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		return _baseEnumerable.ToPooledSet(comparer);
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_count != -1)
		{
			length = _count;
			return true;
		}

		return _baseEnumerable.TryGetNonEnumeratedCount(out length);
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public OrderEnumerator<T, TComparer, TBaseEnumerator> GetEnumerator()
	{
		_baseEnumerable.TryGetNonEnumeratedCount(out var count);
		return new OrderEnumerator<T, TComparer, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _comparer, Math.Max(4, count));
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private OrderedEnumerable<T, TBaseQuery, TBaseEnumerator> GenerateEnumerable()
	{
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			return new OrderedEnumerable<T, T, TBaseQuery, TBaseEnumerator>(_baseEnumerable, EnumerableSorter<T>.IdentityFunc, _comparer, false, null);
		}

		return new OrderedImplicitlyStableEnumerable<T, TBaseQuery, TBaseEnumerator>(_baseEnumerable, false);
	}
}