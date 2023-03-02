using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer> : IOptiQuery<T, OrderEnumerator<T, TComparer, TBaseEnumerator>>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
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
		return new QueryAsEnumerable<T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>>(this);
	}

	public bool Contains<TComparer1>(T item, TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		return _baseEnumerable.Contains(item, comparer);
	}

	public bool Contains(T item)
	{
		return _baseEnumerable.Contains(item);
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
		TryGetNonEnumeratedCount(out _count);

		var temp = EnumerableHelper.ToArray(_baseEnumerable.GetEnumerator(), ArrayPool<T>.Shared, Math.Max(4, _count), out _count);
		var span = temp.AsSpan(0, _count);

		var array = GC.AllocateUninitializedArray<T>(_count);

		span.Sort(_comparer);
		span.CopyTo(array);

		ArrayPool<T>.Shared.Return(temp, RuntimeHelpers.IsReferenceOrContainsReferences<T>());

		length = _count;
		return array;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		comparer ??= EqualityComparer<T>.Default;

		TryGetNonEnumeratedCount(out var count);

		var data = EnumerableHelper.ToArray(_baseEnumerable.GetEnumerator(), ArrayPool<T>.Shared, Math.Max(4, count), out count);
		data.AsSpan(0, count).Sort(_comparer);

		var set = new HashSet<T>(count, comparer);
		ref var first = ref MemoryMarshal.GetArrayDataReference(data);

		for (var i = 0; i < count; i++)
		{
			set.Add(Unsafe.Add(ref first, i));
		}

		_count = count;

		ArrayPool<T>.Shared.Return(data, RuntimeHelpers.IsReferenceOrContainsReferences<T>());

		return set;
	}

	public List<T> ToList()
	{
		TryGetNonEnumeratedCount(out _count);

		var temp = EnumerableHelper.ToArray(_baseEnumerable.GetEnumerator(), ArrayPool<T>.Shared, Math.Max(4, _count), out _count);
		var list = new List<T>(_count);
		var span = temp.AsSpan(0, _count);

		span.Sort(_comparer);
		span.CopyTo(CollectionsMarshal.AsSpan(list));

		ArrayPool<T>.Shared.Return(temp, RuntimeHelpers.IsReferenceOrContainsReferences<T>());

		return list;
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

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator()
	{
		return GetEnumerator();
	}

	private OrderedEnumerable<T, TBaseQuery, TBaseEnumerator> GenerateEnumerable()
	{
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			return new OrderedEnumerable<T, T, TBaseQuery, TBaseEnumerator>(_baseEnumerable, EnumerableSorter<T>.IdentityFunc, _comparer, false, null);
		}

		return new OrderedImplicitlyStableEnumerable<T, TBaseQuery, TBaseEnumerator>(_baseEnumerable, false);
	}
}