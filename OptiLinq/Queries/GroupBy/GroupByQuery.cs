using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer> : IOptiQuery<ArrayQuery<T>, GroupByEnumerator<TKey, T, TComparer>>
	where TKeySelector : struct, IFunction<T, TKey>
	where TBaseEnumerator : IEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TComparer : IEqualityComparer<TKey>
{
	private TBaseQuery _baseQuery;
	private TKeySelector _keySelector;
	private TComparer _comparer;

	internal GroupByQuery(TBaseQuery baseQuery, TKeySelector keySelector, TComparer comparer)
	{
		_baseQuery = baseQuery;
		_keySelector = keySelector;
		_comparer = comparer;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, ArrayQuery<T>, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerable = GetEnumerator();

		while (enumerable.MoveNext())
		{
			seed = func.Eval(in seed, enumerable.Current);
		}

		return selector.Eval(in seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, ArrayQuery<T>, TAccumulate>
	{
		using var enumerable = GetEnumerator();

		while (enumerable.MoveNext())
		{
			seed = @operator.Eval(in seed, enumerable.Current);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<ArrayQuery<T>, bool>
	{
		using var enumerator = GetEnumerator();

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
		return _baseQuery.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<ArrayQuery<T>, bool>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (@operator.Eval(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<ArrayQuery<T>> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer1>(in ArrayQuery<T> item, TComparer1 comparer) where TComparer1 : IEqualityComparer<ArrayQuery<T>>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (comparer.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(in ArrayQuery<T> item)
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (EqualityComparer<ArrayQuery<T>>.Default.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<ArrayQuery<T>> data)
	{
		using var lookup = GetLookup();

		var count = Math.Min(data.Length, lookup.Count);

		for (var i = 0; i < count; i++)
		{
			data[i] = new ArrayQuery<T>(lookup.slots[i].value.ToArray());
		}

		return count;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		var length = TNumber.Zero;

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<TKey, TComparer>(_comparer);

		while (enumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(enumerator.Current)))
			{
				length++;
			}
		}

		return length;
	}

	public int Count()
	{
		return Count<int>();
	}

	public long LongCount()
	{
		return Count<long>();
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<ArrayQuery<T>, bool>
	{
		var count = TNumber.Zero;

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (@operator.Eval(enumerator.Current))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<ArrayQuery<T>, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (predicate(enumerator.Current))
			{
				count++;
			}
		}

		return count;
	}

	public int Count(Func<ArrayQuery<T>, bool> predicate)
	{
		return Count<int>(predicate);
	}

	public long CountLong(Func<ArrayQuery<T>, bool> predicate)
	{
		return Count<long>(predicate);
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out ArrayQuery<T> item) where TIndex : IBinaryInteger<TIndex>
	{
		return EnumerableHelper.TryGetElementAt(GetEnumerator(), index, out item);
	}

	public ArrayQuery<T> ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		throw new ArgumentOutOfRangeException(nameof(index));
	}

	public ArrayQuery<T> ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out ArrayQuery<T> item)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		if (!enumerator.MoveNext())
		{
			item = new ArrayQuery<T>(Array.Empty<T>());
			return false;
		}

		var key = _keySelector.Eval(enumerator.Current);

		var slot = new Slot<TKey, T>(key);
		slot.value.Add(enumerator.Current);

		while (enumerator.MoveNext())
		{
			key = _keySelector.Eval(enumerator.Current);

			if (_comparer.Equals(slot.key, key))
			{
				slot.value.Add(enumerator.Current);
			}
		}

		item = new ArrayQuery<T>(slot.value.ToArray());
		return true;
	}

	public ArrayQuery<T> First()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public ArrayQuery<T> FirstOrDefault()
	{
		using var enumerator = _baseQuery.GetEnumerator();

		if (!enumerator.MoveNext())
		{
			return new ArrayQuery<T>(Array.Empty<T>());
		}

		var key = _keySelector.Eval(enumerator.Current);

		var slot = new Slot<TKey, T>(key);
		slot.value.Add(enumerator.Current);

		while (enumerator.MoveNext())
		{
			key = _keySelector.Eval(enumerator.Current);

			if (_comparer.Equals(slot.key, key))
			{
				slot.value.Add(enumerator.Current);
			}
		}

		return new ArrayQuery<T>(slot.value.ToArray());
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<ArrayQuery<T>>
	{
		throw new NotImplementedException();
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<ArrayQuery<T>>
	{
		using var lookup = GetLookup();

		for (var i = 0; i < lookup.Count; i++)
		{
			@operator.Do(new ArrayQuery<T>(lookup.slots[i].value.ToArray()));
		}
	}

	public bool TryGetLast(out ArrayQuery<T> item)
	{
		using var lookup = GetLookup();

		if (lookup.Count == 0)
		{
			item = new ArrayQuery<T>(Array.Empty<T>());
			return false;
		}

		item = new ArrayQuery<T>(lookup.slots[lookup.Count - 1].value.ToArray());
		return true;
	}

	public ArrayQuery<T> Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public ArrayQuery<T> LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public ArrayQuery<T> Max()
	{
		throw new NotImplementedException();
	}

	public ArrayQuery<T> Min()
	{
		throw new NotImplementedException();
	}

	public bool TryGetSingle(out ArrayQuery<T> item)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		if (!enumerator.MoveNext())
		{
			item = new ArrayQuery<T>(Array.Empty<T>());
			return false;
		}

		var key = _keySelector.Eval(enumerator.Current);

		var slot = new Slot<TKey, T>(key);
		slot.value.Add(enumerator.Current);

		while (enumerator.MoveNext())
		{
			key = _keySelector.Eval(enumerator.Current);

			if (_comparer.Equals(slot.key, key))
			{
				slot.value.Add(enumerator.Current);
			}
			else
			{
				item = new ArrayQuery<T>(Array.Empty<T>());
				return false;
			}
		}

		item = new ArrayQuery<T>(slot.value.ToArray());
		return true;
	}

	public ArrayQuery<T> Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public ArrayQuery<T> SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public ArrayQuery<T>[] ToArray()
	{
		using var lookup = GetLookup();

		var array = new ArrayQuery<T>[lookup.Count];

		for (var i = 0; i < lookup.Count; i++)
		{
			array[i] = new ArrayQuery<T>(lookup.slots[i].value.ToArray());
		}

		return array;
	}

	public ArrayQuery<T>[] ToArray(out int length)
	{
		using var lookup = GetLookup();

		var array = new ArrayQuery<T>[lookup.Count];

		for (var i = 0; i < lookup.Count; i++)
		{
			array[i] = new ArrayQuery<T>(lookup.slots[i].value.ToArray());
		}

		length = array.Length;
		return array;
	}

	public HashSet<ArrayQuery<T>> ToHashSet(IEqualityComparer<ArrayQuery<T>>? comparer = default)
	{
		using var lookup = GetLookup();

		var set = new HashSet<ArrayQuery<T>>(lookup.Count, comparer);

		for (var i = 0; i < lookup.Count; i++)
		{
			set.Add(new ArrayQuery<T>(lookup.slots[i].value.ToArray()));
		}

		return set;
	}

	public List<ArrayQuery<T>> ToList()
	{
		using var lookup = GetLookup();
		var list = new List<ArrayQuery<T>>(lookup.Count);

		for (var i = 0; i < lookup.Count; i++)
		{
			list[i] = new ArrayQuery<T>(lookup.slots[i].value.ToArray());
		}

		return list;
	}

	public PooledList<ArrayQuery<T>> ToPooledList()
	{
		using var lookup = GetLookup();
		var list = new PooledList<ArrayQuery<T>>(lookup.Count);

		for (var i = 0; i < lookup.Count; i++)
		{
			list[i] = new ArrayQuery<T>(lookup.slots[i].value.ToArray());
		}

		return list;
	}

	public PooledQueue<ArrayQuery<T>> ToPooledQueue()
	{
		using var lookup = GetLookup();
		var queue = new PooledQueue<ArrayQuery<T>>(lookup.Count);

		for (var i = 0; i < lookup.Count; i++)
		{
			queue.Enqueue(new ArrayQuery<T>(lookup.slots[i].value.ToArray()));
		}

		return queue;
	}

	public PooledStack<ArrayQuery<T>> ToPooledStack()
	{
		using var lookup = GetLookup();
		var stack = new PooledStack<ArrayQuery<T>>(lookup.Count);

		for (var i = 0; i < lookup.Count; i++)
		{
			stack.Push(new ArrayQuery<T>(lookup.slots[i].value.ToArray()));
		}

		return stack;
	}

	public PooledSet<ArrayQuery<T>, TComparer1> ToPooledSet<TComparer1>(TComparer1 comparer) where TComparer1 : IEqualityComparer<ArrayQuery<T>>
	{
		using var lookup = GetLookup();
		var set = new PooledSet<ArrayQuery<T>, TComparer1>(lookup.Count, comparer);

		for (var i = 0; i < lookup.Count; i++)
		{
			set.Add(new ArrayQuery<T>(lookup.slots[i].value.ToArray()));
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<ArrayQuery<T>> span)
	{
		span = ReadOnlySpan<ArrayQuery<T>>.Empty;
		return false;
	}

	public GroupByEnumerator<TKey, T, TComparer> GetEnumerator()
	{
		return new GroupByEnumerator<TKey, T, TComparer>(GetLookup());
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	IEnumerator<ArrayQuery<T>> IEnumerable<ArrayQuery<T>>.GetEnumerator() => GetEnumerator();

	private Lookup<TKey, T, TComparer> GetLookup()
	{
		var lookup = new Lookup<TKey, T, TComparer>(_comparer);

		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			var key = _keySelector.Eval(enumerator.Current);

			lookup.Add(key, enumerator.Current);
		}

		return lookup;
	}
}