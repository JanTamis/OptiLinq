using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct CountByQuery<T, TKey, TCount, TKeySelector, TComparer, TQuery, TEnumerator> : IOptiQuery<KeyValuePair<TKey, TCount>, CountByEnumerator<TKey, TCount, TComparer>>
	where TKeySelector : struct, IFunction<T, TKey>
	where TComparer : IEqualityComparer<TKey>
	where TQuery : struct, IOptiQuery<T, TEnumerator>
	where TEnumerator : IEnumerator<T>
	where TCount : INumberBase<TCount>
	where TKey : notnull
{
	private TQuery _baseQuery;
	private TKeySelector _keySelector;

	public CountByQuery(TQuery baseQuery, TKeySelector keySelector)
	{
		_baseQuery = baseQuery;
		_keySelector = keySelector;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, KeyValuePair<TKey, TCount>, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var dictionary = GetDictionary();
		using var enumerator = dictionary.GetEnumerator();

		while (enumerator.MoveNext())
		{
			seed = func.Eval(in seed, enumerator.Current);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, KeyValuePair<TKey, TCount>, TAccumulate>
	{
		using var dictionary = GetDictionary();
		using var enumerator = dictionary.GetEnumerator();

		while (enumerator.MoveNext())
		{
			seed = @operator.Eval(in seed, enumerator.Current);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<KeyValuePair<TKey, TCount>, bool>
	{
		using var dictionary = GetDictionary();
		using var enumerator = dictionary.GetEnumerator();

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

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<KeyValuePair<TKey, TCount>, bool>
	{
		using var dictionary = GetDictionary();
		using var enumerator = dictionary.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (@operator.Eval(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<KeyValuePair<TKey, TCount>> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer1>(in KeyValuePair<TKey, TCount> item, TComparer1 comparer) where TComparer1 : IEqualityComparer<KeyValuePair<TKey, TCount>>
	{
		using var dictionary = GetDictionary();
		using var enumerator = dictionary.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (comparer.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(in KeyValuePair<TKey, TCount> item)
	{
		using var dictionary = GetDictionary();
		using var enumerator = dictionary.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (EqualityComparer<KeyValuePair<TKey, TCount>>.Default.Equals(enumerator.Current, item)) ;
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<KeyValuePair<TKey, TCount>> data)
	{
		using var dictionary = GetDictionary();
		using var enumerator = dictionary.GetEnumerator();

		var i = 0;

		while (enumerator.MoveNext())
		{
			data[i++] = enumerator.Current;
		}

		return i;
	}

	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		using var set = new PooledSet<TKey, TComparer>();
		using var enumerator = _baseQuery.GetEnumerator();

		var count = TNumber.Zero;

		while (enumerator.MoveNext())
		{
			if (set.Add(_keySelector.Eval(enumerator.Current)))
			{
				count++;
			}
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

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TCountOperator : struct, IFunction<KeyValuePair<TKey, TCount>, bool> where TNumber : INumberBase<TNumber>
	{
		using var dictionary = GetDictionary();
		using var enumerator = dictionary.GetEnumerator();

		var count = TNumber.Zero;

		while (enumerator.MoveNext())
		{
			if (@operator.Eval(enumerator.Current))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<KeyValuePair<TKey, TCount>, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		using var dictionary = GetDictionary();
		using var enumerator = dictionary.GetEnumerator();

		var count = TNumber.Zero;

		while (enumerator.MoveNext())
		{
			if (predicate(enumerator.Current))
			{
				count++;
			}
		}

		return count;
	}

	public int Count(Func<KeyValuePair<TKey, TCount>, bool> predicate)
	{
		return Count<int>(predicate);
	}

	public long CountLong(Func<KeyValuePair<TKey, TCount>, bool> predicate)
	{
		return Count<long>(predicate);
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out KeyValuePair<TKey, TCount> item) where TIndex : IBinaryInteger<TIndex>
	{
		using var dictionary = GetDictionary();

		if (TIndex.IsPositive(index))
		{
			var i = Int32.CreateChecked(index);

			if (i < dictionary.Count)
			{
				var result = dictionary._slots[i];

				item = new KeyValuePair<TKey, TCount>(result.Key, result.Value);
				return true;
			}
		}

		item = default;
		return false;
	}

	public KeyValuePair<TKey, TCount> ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		throw new ArgumentOutOfRangeException(nameof(index));
	}

	public KeyValuePair<TKey, TCount> ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		return default;
	}

	public bool TryGetFirst(out KeyValuePair<TKey, TCount> item)
	{
		using var dictionary = GetDictionary();

		if (dictionary.Count > 0)
		{
			var result = dictionary._slots[0];

			item = new KeyValuePair<TKey, TCount>(result.Key, result.Value);
			return true;
		}

		item = default;
		return false;
	}

	public KeyValuePair<TKey, TCount> First()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		throw new InvalidOperationException();
	}

	public KeyValuePair<TKey, TCount> FirstOrDefault()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		return default;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<KeyValuePair<TKey, TCount>>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		using var dictionary = GetDictionary();

		for (var i = 0; i < dictionary.Count; i++)
		{
			var item = dictionary._slots[i];
			Task.Factory.StartNew(x => @operator.Do(Unsafe.Unbox<KeyValuePair<TKey, TCount>>(x!)), new KeyValuePair<TKey, TCount>(item.Key, item.Value), token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<KeyValuePair<TKey, TCount>>
	{
		using var dictionary = GetDictionary();

		for (var i = 0; i < dictionary.Count; i++)
		{
			var item = dictionary._slots[i];
			@operator.Do(new KeyValuePair<TKey, TCount>(item.Key, item.Value));
		}
	}

	public bool TryGetLast(out KeyValuePair<TKey, TCount> item)
	{
		using var dictionary = GetDictionary();

		if (dictionary.Count > 0)
		{
			var result = dictionary._slots[dictionary.Count - 1];

			item = new KeyValuePair<TKey, TCount>(result.Key, result.Value);
			return true;
		}

		item = default;
		return false;
	}

	public KeyValuePair<TKey, TCount> Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException();
	}

	public KeyValuePair<TKey, TCount> LastOrDefault()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		return default;
	}

	public KeyValuePair<TKey, TCount> Max()
	{
		return EnumerableHelper.Max<KeyValuePair<TKey, TCount>, CountByEnumerator<TKey, TCount, TComparer>>(GetEnumerator());
	}

	public KeyValuePair<TKey, TCount> Min()
	{
		return EnumerableHelper.Min<KeyValuePair<TKey, TCount>, CountByEnumerator<TKey, TCount, TComparer>>(GetEnumerator());
	}

	public bool TryGetSingle(out KeyValuePair<TKey, TCount> item)
	{
		using var dictionary = GetDictionary();

		if (dictionary.Count == 1)
		{
			var result = dictionary._slots[0];

			item = new KeyValuePair<TKey, TCount>(result.Key, result.Value);
			return true;
		}

		item = default;
		return false;
	}

	public KeyValuePair<TKey, TCount> Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException();
	}

	public KeyValuePair<TKey, TCount> SingleOrDefault()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		return default;
	}

	public KeyValuePair<TKey, TCount>[] ToArray()
	{
		using var dictionary = GetDictionary();

		var result = new KeyValuePair<TKey, TCount>[dictionary.Count];

		for (var i = 0; i < dictionary.Count; i++)
		{
			var item = dictionary._slots[i];
			result[i] = new KeyValuePair<TKey, TCount>(item.Key, item.Value);
		}

		return result;
	}

	public KeyValuePair<TKey, TCount>[] ToArray(out int length)
	{
		var result = ToArray();

		length = result.Length;
		return result;
	}

	public HashSet<KeyValuePair<TKey, TCount>> ToHashSet(IEqualityComparer<KeyValuePair<TKey, TCount>>? comparer = default)
	{
		using var dictionary = GetDictionary();

		var result = new HashSet<KeyValuePair<TKey, TCount>>(comparer);

		for (var i = 0; i < dictionary.Count; i++)
		{
			var item = dictionary._slots[i];
			result.Add(new KeyValuePair<TKey, TCount>(item.Key, item.Value));
		}

		return result;
	}

	public List<KeyValuePair<TKey, TCount>> ToList()
	{
		using var dictionary = GetDictionary();

		var result = new List<KeyValuePair<TKey, TCount>>(dictionary.Count);

		for (var i = 0; i < dictionary.Count; i++)
		{
			var item = dictionary._slots[i];
			result.Add(new KeyValuePair<TKey, TCount>(item.Key, item.Value));
		}

		return result;
	}

	public PooledList<KeyValuePair<TKey, TCount>> ToPooledList()
	{
		using var dictionary = GetDictionary();

		var result = new PooledList<KeyValuePair<TKey, TCount>>(dictionary.Count);

		for (var i = 0; i < dictionary.Count; i++)
		{
			var item = dictionary._slots[i];
			result.Add(new KeyValuePair<TKey, TCount>(item.Key, item.Value));
		}

		return result;
	}

	public PooledQueue<KeyValuePair<TKey, TCount>> ToPooledQueue()
	{
		using var dictionary = GetDictionary();

		var result = new PooledQueue<KeyValuePair<TKey, TCount>>(dictionary.Count);

		for (var i = 0; i < dictionary.Count; i++)
		{
			var item = dictionary._slots[i];
			result.Enqueue(new KeyValuePair<TKey, TCount>(item.Key, item.Value));
		}

		return result;
	}

	public PooledStack<KeyValuePair<TKey, TCount>> ToPooledStack()
	{
		using var dictionary = GetDictionary();

		var result = new PooledStack<KeyValuePair<TKey, TCount>>(dictionary.Count);

		for (var i = 0; i < dictionary.Count; i++)
		{
			var item = dictionary._slots[i];
			result.Push(new KeyValuePair<TKey, TCount>(item.Key, item.Value));
		}

		return result;
	}

	public PooledSet<KeyValuePair<TKey, TCount>, TComparer1> ToPooledSet<TComparer1>(TComparer1 comparer) where TComparer1 : IEqualityComparer<KeyValuePair<TKey, TCount>>
	{
		using var dictionary = GetDictionary();

		var result = new PooledSet<KeyValuePair<TKey, TCount>, TComparer1>(dictionary.Count, comparer);

		for (var i = 0; i < dictionary.Count; i++)
		{
			var item = dictionary._slots[i];
			result.Add(new KeyValuePair<TKey, TCount>(item.Key, item.Value));
		}

		return result;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<KeyValuePair<TKey, TCount>> span)
	{
		span = default;
		return false;
	}

	public CountByEnumerator<TKey, TCount, TComparer> GetEnumerator()
	{
		return new CountByEnumerator<TKey, TCount, TComparer>(GetDictionary());
	}

	IEnumerator<KeyValuePair<TKey, TCount>> IEnumerable<KeyValuePair<TKey, TCount>>.GetEnumerator() => GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private PooledDictionary<TKey, TCount, TComparer> GetDictionary()
	{
		using var enumerator = _baseQuery.GetEnumerator();

		var dictionary = new PooledDictionary<TKey, TCount, TComparer>();

		while (enumerator.MoveNext())
		{
			var key = _keySelector.Eval(enumerator.Current);

			ref var result = ref dictionary.TryGetValue(key);

			if (Unsafe.IsNullRef(ref result))
			{
				dictionary.Add(key, TCount.One);
			}
			else
			{
				result++;
			}
		}

		return dictionary;
	}
}