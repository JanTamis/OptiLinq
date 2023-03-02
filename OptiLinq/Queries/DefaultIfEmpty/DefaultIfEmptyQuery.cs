using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, DefaultIfEmptyEnumerator<T, TBaseEnumerator>>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private TBaseQuery _baseQuery;
	private readonly T _defaultValue;

	internal DefaultIfEmptyQuery(ref TBaseQuery baseQuery, in T defaultValue)
	{
		_baseQuery = baseQuery;
		_defaultValue = defaultValue;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(func, selector, seed);
		}

		return selector.Eval(func.Eval(seed, _defaultValue));
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.Aggregate(@operator, seed);
		}

		return @operator.Eval(seed, _defaultValue);
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.All(@operator);
		}

		return @operator.Eval(_defaultValue);
	}

	public bool Any()
	{
		return true;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.Any(@operator);
		}

		return @operator.Eval(_defaultValue);
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>>(this);
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.Contains(item, comparer);
		}

		return comparer.Equals(item, _defaultValue);
	}

	public bool Contains(T item)
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.Contains(item);
		}

		return EqualityComparer<T>.Default.Equals(item, _defaultValue);
	}

	public int CopyTo(Span<T> data)
	{
		var count = _baseQuery.CopyTo(data);

		if (count == 0 && data.Length > 0)
		{
			data[0] = default!;
			count = 1;
		}

		return count;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		var count = _baseQuery.Count<TNumber>();

		if (TNumber.IsZero(count))
		{
			count = TNumber.One;
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

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.TryGetElementAt(index, out item);
		}

		if (TIndex.IsZero(index))
		{
			item = _defaultValue;
			return true;
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
		if (_baseQuery.TryGetFirst(out item))
		{
			item = _defaultValue;
		}

		return true;
	}

	public T First()
	{
		if (_baseQuery.TryGetFirst(out var item))
		{
			item = _defaultValue;
		}

		return item;
	}

	public T FirstOrDefault()
	{
		if (_baseQuery.TryGetFirst(out var item))
		{
			item = _defaultValue;
		}

		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		if (_baseQuery.Any())
		{
			_baseQuery.ForAll(@operator, token);
		}
		else
		{
			var defaultValue = _defaultValue;
			Task.Factory.StartNew(() => @operator.Do(defaultValue), token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		if (_baseQuery.Any())
		{
			_baseQuery.ForEach(@operator);
		}

		@operator.Do(_defaultValue);
	}

	public bool TryGetLast(out T item)
	{
		if (!_baseQuery.TryGetLast(out item))
		{
			item = _defaultValue;
		}

		return true;
	}

	public T Last()
	{
		TryGetLast(out var item);
		return item;
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, DefaultIfEmptyEnumerator<T, TBaseEnumerator>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, DefaultIfEmptyEnumerator<T, TBaseEnumerator>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		if (!_baseQuery.TryGetSingle(out item))
		{
			item = _defaultValue;
		}

		return true;
	}

	public T Single()
	{
		if (!_baseQuery.TryGetSingle(out var item))
		{
			item = _defaultValue;
		}

		return item;
	}

	public T SingleOrDefault()
	{
		if (!_baseQuery.TryGetSingle(out var item))
		{
			item = _defaultValue;
		}

		return item;
	}

	public T[] ToArray()
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.ToArray();
		}

		return new[] { _defaultValue };
	}

	public T[] ToArray(out int length)
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.ToArray(out length);
		}

		length = 1;
		return new[] { _defaultValue };
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.ToHashSet(comparer);
		}

		return new HashSet<T>(comparer)
		{
			_defaultValue,
		};
	}

	public List<T> ToList()
	{
		if (_baseQuery.Any())
		{
			return _baseQuery.ToList();
		}

		return new List<T>
		{
			_defaultValue,
		};
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_baseQuery.TryGetNonEnumeratedCount(out length))
		{
			return true;
		}

		if (!_baseQuery.Any())
		{
			length = 1;
			return true;
		}

		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		if (_baseQuery.TryGetSpan(out span))
		{
			return true;
		}

		return false;
	}

	public DefaultIfEmptyEnumerator<T, TBaseEnumerator> GetEnumerator()
	{
		return new DefaultIfEmptyEnumerator<T, TBaseEnumerator>(_baseQuery.GetEnumerator(), _defaultValue);
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();
}