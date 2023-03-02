using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct EnumerableQuery<T> : IOptiQuery<T, EnumerableEnumerator<T>>
{
	private readonly IEnumerable<T> _enumerable;

	internal EnumerableQuery(IEnumerable<T> enumerable)
	{
		_enumerable = enumerable;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerable = _enumerable.GetEnumerator();

		while (enumerable.MoveNext())
		{
			seed = func.Eval(seed, enumerable.Current);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var enumerable = _enumerable.GetEnumerator();

		while (enumerable.MoveNext())
		{
			seed = @operator.Eval(seed, enumerable.Current);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _enumerable.GetEnumerator();

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
		return _enumerable.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _enumerable.GetEnumerator();

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
		return _enumerable;
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		if (comparer == null)
		{
			foreach (var element in _enumerable)
			{
				if (EqualityComparer<T>.Default.Equals(element, item)) // benefits from devirtualization and likely inlining
				{
					return true;
				}
			}
		}
		else
		{
			foreach (var element in _enumerable)
			{
				if (comparer.Equals(element, item))
				{
					return true;
				}
			}
		}

		return false;
	}

	public bool Contains(T item)
	{
		return _enumerable.Contains(item);
	}

	public int CopyTo(Span<T> data)
	{
		using var enumerator = _enumerable.GetEnumerator();
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
		using var enumerator = _enumerable.GetEnumerator();

		var count = TNumber.Zero;

		while (enumerator.MoveNext())
		{
			count++;
		}

		return count;
	}

	public int Count()
	{
		return _enumerable.Count();
	}

	public long LongCount()
	{
		return _enumerable.LongCount();
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = _enumerable.GetEnumerator();

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

		item = default;
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
		using var enumerator = _enumerable.GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;
			return true;
		}

		item = default;
		return false;
	}

	public T First()
	{
		return _enumerable.First();
	}

	public T FirstOrDefault()
	{
		return _enumerable.FirstOrDefault();
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = _enumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		foreach (var item in _enumerable)
		{
			@operator.Do(in item);
		}
	}

	public bool TryGetLast(out T item)
	{
		throw new NotImplementedException();
	}

	public T Last()
	{
		return _enumerable.Last();
	}

	public T LastOrDefault()
	{
		return _enumerable.LastOrDefault();
	}

	public T Max()
	{
		return _enumerable.Max();
	}

	public T Min()
	{
		return _enumerable.Min();
	}

	public bool TryGetSingle(out T item)
	{
		using var enumerator = _enumerable.GetEnumerator();

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
		return _enumerable.Single();
	}

	public T SingleOrDefault()
	{
		return _enumerable.SingleOrDefault();
	}

	public T[] ToArray()
	{
		return _enumerable.ToArray();
	}

	public T[] ToArray(out int length)
	{
		length = 0;
		return Array.Empty<T>();
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		return _enumerable.ToHashSet(comparer);
	}

	public List<T> ToList()
	{
		return _enumerable.ToList();
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		return _enumerable.TryGetNonEnumeratedCount(out length);
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		if (_enumerable.GetType() == typeof(T[]))
		{
			span = Unsafe.As<T[]>(_enumerable);
			return true;
		}

		if (_enumerable.GetType() == typeof(List<T>))
		{
			span = CollectionsMarshal.AsSpan(Unsafe.As<List<T>>(_enumerable));
			return true;
		}

		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public EnumerableEnumerator<T> GetEnumerator()
	{
		return new EnumerableEnumerator<T>(_enumerable.GetEnumerator());
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator()
	{
		return GetEnumerator();
	}
}