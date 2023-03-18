using System.Buffers;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ReverseQuery<T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, ReverseEnumerator<T>>
	where TBaseEnumerator : IEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private TBaseQuery _baseQuery;

	public ReverseQuery(ref TBaseQuery baseQuery)
	{
		_baseQuery = baseQuery;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		return _baseQuery.Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(func, selector, seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		return _baseQuery.Aggregate(@operator, seed);
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		return _baseQuery.All(@operator);
	}

	public bool Any()
	{
		return _baseQuery.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		return _baseQuery.Any(@operator);
	}

	public IEnumerable<T> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return _baseQuery.Contains(item, comparer);
	}

	public bool Contains(in T item)
	{
		return _baseQuery.Contains(item);
	}
	
	public int CopyTo(Span<T> data)
	{
		var length = _baseQuery.CopyTo(data);

		var temp = data.Slice(0, length);
		temp.Reverse();

		return length;
	}

	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return _baseQuery.Count<TNumber>();
	}

	public int Count()
	{
		return _baseQuery.Count();
	}

	public long LongCount()
	{
		return _baseQuery.LongCount();
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T, bool>
	{
		return _baseQuery.Count<TCountOperator, TNumber>(@operator);
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		return _baseQuery.Count<TNumber>(predicate);
	}

	public int Count(Func<T, bool> predicate)
	{
		return _baseQuery.Count(predicate);
	}

	public long CountLong(Func<T, bool> predicate)
	{
		return _baseQuery.CountLong(predicate);
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
		return _baseQuery.TryGetLast(out item);
	}

	public T First()
	{
		return _baseQuery.Last();
	}

	public T FirstOrDefault()
	{
		return _baseQuery.LastOrDefault();
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		return _baseQuery.ForAll(@operator, token);
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		TryGetNonEnumeratedCount(out var count);

		var data = EnumerableHelper.ToArray(_baseQuery.GetEnumerator(), ArrayPool<T>.Shared, Math.Max(4, count), out count);
		data.AsSpan(0, count).Reverse();

		ref var first = ref MemoryMarshal.GetArrayDataReference(data);

		for (var i = 0; i < count; i++)
		{
			@operator.Do(Unsafe.Add(ref first, i));
		}

		ArrayPool<T>.Shared.Return(data, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
	}

	public bool TryGetLast(out T item)
	{
		return TryGetFirst(out item);
	}

	public T Last()
	{
		return _baseQuery.First();
	}

	public T LastOrDefault()
	{
		return _baseQuery.FirstOrDefault();
	}

	public T Max()
	{
		return _baseQuery.Max();
	}

	public T Min()
	{
		return _baseQuery.Min();
	}

	public bool TryGetSingle(out T item)
	{
		return _baseQuery.TryGetSingle(out item);
	}

	public T Single()
	{
		return _baseQuery.Single();
	}

	public T SingleOrDefault()
	{
		return _baseQuery.SingleOrDefault();
	}

	public T[] ToArray()
	{
		var array = _baseQuery.ToArray();
		Array.Reverse(array);

		return array;
	}

	public T[] ToArray(out int length)
	{
		var array = _baseQuery.ToArray(out length);
		Array.Reverse(array, 0, length);

		return array;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		TryGetNonEnumeratedCount(out var count);

		var data = EnumerableHelper.ToArray(_baseQuery.GetEnumerator(), ArrayPool<T>.Shared, Math.Max(4, count), out count);
		data.AsSpan(0, count).Reverse();

		var set = new HashSet<T>(count, comparer);
		ref var first = ref MemoryMarshal.GetArrayDataReference(data);

		for (var i = 0; i < count; i++)
		{
			set.Add(Unsafe.Add(ref first, i));
		}

		ArrayPool<T>.Shared.Return(data, RuntimeHelpers.IsReferenceOrContainsReferences<T>());

		return set;
	}

	public List<T> ToList()
	{
		var list = _baseQuery.ToList();
		list.Reverse();

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = _baseQuery.ToPooledList();

		list.Items.AsSpan(0, list.Count).Reverse();

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = _baseQuery.ToPooledQueue();

		queue._array.AsSpan(0, queue.Count).Reverse();

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = _baseQuery.ToPooledStack();

		stack._array.AsSpan(0, stack.Count).Reverse();

		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return _baseQuery.ToPooledSet(comparer);
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		return _baseQuery.TryGetNonEnumeratedCount(out length);
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public ReverseEnumerator<T> GetEnumerator()
	{
		return new ReverseEnumerator<T>(_baseQuery.ToPooledList());
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}