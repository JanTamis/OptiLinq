using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct MemoizeQuery<T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, MemoizeEnumerator<T, TBaseEnumerator>>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private List<T>? _cache;
	private readonly object _locker;
	private TBaseQuery _baseEnumerable;

	internal MemoizeQuery(ref TBaseQuery baseEnumerable)
	{
		_baseEnumerable = baseEnumerable;
		_cache = null;
		_locker = new object();
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				_cache = new List<T>();
				using var enumerable = _baseEnumerable.GetEnumerator();

				while (enumerable.MoveNext())
				{
					seed = func.Eval(seed, enumerable.Current);
					_cache.Add(enumerable.Current);
				}

				return selector.Eval(seed);
			}

			var span = CollectionsMarshal.AsSpan(_cache);

			foreach (var item in span)
			{
				seed = func.Eval(seed, item);
			}

			return selector.Eval(seed);
		}
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				_cache = new List<T>();
				using var enumerable = _baseEnumerable.GetEnumerator();

				while (enumerable.MoveNext())
				{
					seed = @operator.Eval(seed, enumerable.Current);
					_cache.Add(enumerable.Current);
				}

				return seed;
			}

			var span = CollectionsMarshal.AsSpan(_cache);

			foreach (var item in span)
			{
				seed = @operator.Eval(seed, item);
			}

			return seed;
		}
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				_cache = new List<T>();
				using var enumerator = _baseEnumerable.GetEnumerator();

				var result = true;

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);

					if (result && !@operator.Eval(enumerator.Current))
					{
						result = false;
					}
				}

				return result;
			}

			var span = CollectionsMarshal.AsSpan(_cache);

			foreach (var item in span)
			{
				if (!@operator.Eval(item))
				{
					return false;
				}
			}

			return true;
		}
	}

	public bool Any()
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				return _baseEnumerable.Any();
			}

			return _cache.Count > 0;
		}
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				_cache = new List<T>();
				using var enumerator = _baseEnumerable.GetEnumerator();

				var result = false;

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);

					if (!result && @operator.Eval(enumerator.Current))
					{
						result = true;
					}
				}

				return result;
			}

			var span = CollectionsMarshal.AsSpan(_cache);

			foreach (var item in span)
			{
				if (@operator.Eval(item))
				{
					return true;
				}
			}

			return false;
		}
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>>(this);
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				return _baseEnumerable.Contains(item, comparer);
			}

			var span = CollectionsMarshal.AsSpan(_cache);

			foreach (var cachedItem in span)
			{
				if (comparer.Equals(cachedItem, item))
				{
					return true;
				}
			}

			return false;
		}
	}

	public bool Contains(T item)
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				return _baseEnumerable.Contains(item);
			}

			var span = CollectionsMarshal.AsSpan(_cache);

			foreach (var cachedItem in span)
			{
				if (EqualityComparer<T>.Default.Equals(cachedItem, item))
				{
					return true;
				}
			}

			return false;
		}
	}

	public int CopyTo(Span<T> data)
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				return _baseEnumerable.CopyTo(data);
			}

			var length = Math.Min(data.Length, _cache.Count);
			var span = CollectionsMarshal.AsSpan(_cache).Slice(length);

			span.CopyTo(data);

			return length;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				_cache = new List<T>();
				using var enumerator = _baseEnumerable.GetEnumerator();

				var result = TNumber.Zero;

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);

					result++;
				}

				return result;
			}

			return TNumber.CreateChecked(_cache.Count);
		}
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
		lock (_locker)
		{
			if (_cache is null)
			{
				return _baseEnumerable.TryGetElementAt(index, out item);
			}

			if (index > TIndex.CreateChecked(_cache.Count))
			{
				throw ThrowHelper.CreateOutOfRangeException();
			}

			item = _cache[Int32.CreateChecked(index)];
			return true;
		}
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		throw ThrowHelper.CreateOutOfRangeException();
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out T item)
	{
		lock (_locker)
		{
			if (_cache is { Count: > 0 })
			{
				item = _cache[0];
				return true;
			}

			return _baseEnumerable.TryGetFirst(out item);
		}
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
		lock (_locker)
		{
			var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

			if (_cache is null)
			{
				using var enumerator = _baseEnumerable.GetEnumerator();
				_cache = new List<T>();

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);
					Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
				}
			}
			else
			{
				var span = CollectionsMarshal.AsSpan(_cache);

				foreach (var item in span)
				{
					Task.Factory.StartNew(x => @operator.Do((T)x), item, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
				}
			}

			schedulerPair.Complete();
			return schedulerPair.Completion;
		}
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				using var enumerator = _baseEnumerable.GetEnumerator();
				_cache = new List<T>();

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);
					@operator.Do(enumerator.Current);
				}
			}
			else
			{
				var span = CollectionsMarshal.AsSpan(_cache);

				foreach (var item in span)
				{
					@operator.Do(item);
				}
			}
		}
	}

	public bool TryGetLast(out T item)
	{
		lock (_locker)
		{
			if (_cache is null)
			{
				using var enumerable = _baseEnumerable.GetEnumerator();
				_cache = new List<T>();

				if (!enumerable.MoveNext())
				{
					item = default!;
					return false;
				}

				item = enumerable.Current;
				_cache.Add(item);

				while (enumerable.MoveNext())
				{
					_cache.Add(enumerable.Current);

					item = enumerable.Current;
				}

				return true;
			}

			if (_cache.Count is 0)
			{
				item = default!;
				return false;
			}

			item = _cache[^1];
			return true;
		}
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
		return EnumerableHelper.Max<T, MemoizeEnumerator<T, TBaseEnumerator>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, MemoizeEnumerator<T, TBaseEnumerator>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		lock (_locker)
		{
			if (_cache is [var first])
			{
				item = first;
				return true;
			}

			return _baseEnumerable.TryGetSingle(out item);
		}
	}

	public T Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence contains no elements");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		lock (_locker)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			if (_cache is null)
			{
				_cache = _baseEnumerable.TryGetNonEnumeratedCount(out var count)
					? new List<T>(count)
					: new List<T>();

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);
				}
			}

			return _cache.ToArray();
		}
	}

	public T[] ToArray(out int length)
	{
		lock (_locker)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			if (_cache is null)
			{
				_cache = _baseEnumerable.TryGetNonEnumeratedCount(out var count)
					? new List<T>(count)
					: new List<T>();

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);
				}
			}

			length = _cache.Count;
			return _cache.ToArray();
		}
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = null)
	{
		lock (_locker)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			if (_cache is null)
			{
				_cache = _baseEnumerable.TryGetNonEnumeratedCount(out var count)
					? new List<T>(count)
					: new List<T>();

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);
				}
			}

			return new HashSet<T>(_cache, comparer);
		}
	}

	public List<T> ToList()
	{
		lock (_locker)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			if (_cache is null)
			{
				_cache = _baseEnumerable.TryGetNonEnumeratedCount(out var count)
					? new List<T>(count)
					: new List<T>();

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);
				}
			}

			return new List<T>(_cache);
		}
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		return _baseEnumerable.TryGetNonEnumeratedCount(out length);
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		lock (_locker)
		{
			if (_cache is not null)
			{
				span = CollectionsMarshal.AsSpan(_cache);
				return true;
			}

			if (_baseEnumerable.TryGetSpan(out span))
			{
				_cache = new List<T>(span.Length);
				span.CopyTo(CollectionsMarshal.AsSpan(_cache));

				return true;
			}
		}

		return false;
	}

	public MemoizeEnumerator<T, TBaseEnumerator> GetEnumerator()
	{
		return new MemoizeEnumerator<T, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _locker, ref _cache);
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();
}