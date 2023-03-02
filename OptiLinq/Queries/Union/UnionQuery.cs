using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TSecondQuery, TComparer> : IOptiQuery<T, UnionEnumerator<T, TComparer>>
	where TFirstQuery : struct, IOptiQuery<T>
	where TSecondQuery : struct, IOptiQuery<T>
	where TComparer : IEqualityComparer<T>
{
	private TFirstQuery _firstQuery;
	private TSecondQuery _secondQuery;
	private readonly TComparer _comparer;

	private int _count = -1;

	internal UnionQuery(ref TFirstQuery firstQuery, ref TSecondQuery secondQuery, TComparer comparer)
	{
		_firstQuery = firstQuery;
		_secondQuery = secondQuery;
		_comparer = comparer;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		_count = 0;

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				seed = func.Eval(seed, enumerator.Current);
				_count++;
			}
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		_count = 0;

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				seed = @operator.Eval(seed, enumerator.Current);
				_count++;
			}
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current) && !@operator.Eval(enumerator.Current))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current) && @operator.Eval(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>>(this);
	}

	public bool Contains<TComparer1>(T item, TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current) && comparer.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(T item)
	{
		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current) && EqualityComparer<T>.Default.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		var index = 0;

		while (enumerator.MoveNext() && index < data.Length)
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				data[index++] = enumerator.Current;
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

		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				result++;
			}
		}

		if (typeof(TNumber) == typeof(int) || typeof(TNumber) == typeof(long))
		{
			_count = Int32.CreateTruncating(result);
		}

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

		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
				_count++;
			}
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		if (TryGetNonEnumeratedCount(out _count) && _count == 0)
		{
			return;
		}

		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				@operator.Do(enumerator.Current);
				_count++;
			}
		}
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
		return EnumerableHelper.Max<T, UnionEnumerator<T, TComparer>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, UnionEnumerator<T, TComparer>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		if (_firstQuery.Any())
		{
			return _firstQuery.TryGetFirst(out item);
		}

		return _secondQuery.TryGetSingle(out item);
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
		using var enumerator = _firstQuery.GetEnumerator();
		using var set = GetSet();

		if (TryGetNonEnumeratedCount(out _count))
		{
			var array = new T[_count];
			var index = 0;

			while (enumerator.MoveNext() && index < _count)
			{
				if (set.AddIfNotPresent(enumerator.Current))
				{
					array[index++] = enumerator.Current;
				}
			}

			return array;
		}

		var builder = new PooledList<T>();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				builder.Add(enumerator.Current);
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

		using var valueSet = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (valueSet.AddIfNotPresent(enumerator.Current))
			{
				set.Add(enumerator.Current);
			}
		}

		return set;
	}

	public List<T> ToList()
	{
		var list = TryGetNonEnumeratedCount(out var count)
			? new List<T>(count)
			: new List<T>();

		using var valueSet = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (valueSet.AddIfNotPresent(enumerator.Current))
			{
				list.Add(enumerator.Current);
			}
		}

		return list;
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

	public UnionEnumerator<T, TComparer> GetEnumerator()
	{
		_firstQuery.TryGetNonEnumeratedCount(out var count1);
		_secondQuery.TryGetNonEnumeratedCount(out var count2);

		return new UnionEnumerator<T, TComparer>(_firstQuery.GetEnumerator(), _secondQuery.GetEnumerator(), _comparer, count1 + count2);
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();

	private PooledSet<T, TComparer> GetSet()
	{
		_firstQuery.TryGetNonEnumeratedCount(out var count1);
		_secondQuery.TryGetNonEnumeratedCount(out var count2);

		var set = new PooledSet<T, TComparer>(count1 + count2, _comparer);
		using var enumerator = _secondQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.AddIfNotPresent(enumerator.Current);
		}

		return set;
	}
}