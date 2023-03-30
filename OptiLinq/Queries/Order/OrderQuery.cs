using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer> : IOptiQuery<T, OrderEnumerator<T>>
	where TBaseEnumerator : IEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TComparer : IOptiComparer<T>
{
	private TBaseQuery _baseEnumerable;
	private readonly TComparer _comparer;

	internal OrderQuery(ref TBaseQuery baseEnumerable, TComparer comparer)
	{
		_baseEnumerable = baseEnumerable;
		_comparer = comparer;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var buffer = ToPooledList();

		foreach (var item in buffer.AsSpan())
		{
			seed = func.Eval(in seed, in item);
		}

		return selector.Eval(in seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var buffer = ToPooledList();

		foreach (var item in buffer.AsSpan())
		{
			seed = @operator.Eval(in seed, in item);
		}

		return seed;
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
		var count = _baseEnumerable.CopyTo(data);

		Sorter<T, TComparer>.Sort(data.Slice(0, count), _comparer);

		return count;
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
			var buffer = _baseEnumerable.ToPooledList();
			var intIndex = Int32.CreateChecked(index);

			Sorter<T, TComparer>.Sort(buffer.AsSpan(), _comparer);

			if (intIndex < buffer.Count)
			{
				item = buffer[intIndex];
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
		using var buffer = _baseEnumerable.ToPooledList();
		var bufferSpan = buffer.AsSpan();

		Sorter<T, TComparer>.Sort(bufferSpan, _comparer);

		for (var i = 0; i < bufferSpan.Length; i++)
		{
			@operator.Do(in bufferSpan[i]);
		}
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
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (!enumerator.MoveNext())
		{
			item = default!;
			return false;
		}

		item = enumerator.Current;

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

		throw new InvalidOperationException("Sequence does not contain exactly one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		var result = _baseEnumerable.ToArray();

		Sorter<T, TComparer>.Sort(result, _comparer);

		return result;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		return _baseEnumerable.ToHashSet(comparer);
	}

	public List<T> ToList()
	{
		var result = _baseEnumerable.ToList();

		Sorter<T, TComparer>.Sort(CollectionsMarshal.AsSpan(result), _comparer);

		return result;
	}

	public PooledList<T> ToPooledList()
	{
		var result = _baseEnumerable.ToPooledList();

		Sorter<T, TComparer>.Sort(result.AsSpan(), _comparer);

		return result;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		using var buffer = ToPooledList();
		var result = new PooledQueue<T>(buffer.Count);

		for (var i = 0; i < buffer.Count; i++)
		{
			result.Enqueue(buffer[i]);
		}

		return result;
	}

	public PooledStack<T> ToPooledStack()
	{
		using var buffer = ToPooledList();
		var result = new PooledStack<T>(buffer.Count);

		for (var i = 0; i < buffer.Count; i++)
		{
			result.Push(buffer[i]);
		}

		return result;
	}

	public PooledSet<T, TComparer1> ToPooledSet<TComparer1>(TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		return _baseEnumerable.ToPooledSet(comparer);
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		return _baseEnumerable.TryGetNonEnumeratedCount(out length);
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public OrderEnumerator<T> GetEnumerator()
	{
		return new OrderEnumerator<T>(ToPooledList());
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}