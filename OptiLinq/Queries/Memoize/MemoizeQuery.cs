using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct MemoizeQuery<T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, MemoizeEnumerator<T, TBaseEnumerator>>, IDisposable
	where TBaseEnumerator : IEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private PooledList<T> _cache;
	private bool _cacheInitialized = false;
	private readonly object _locker;
	private TBaseQuery _baseEnumerable;

	internal MemoizeQuery(ref TBaseQuery baseEnumerable)
	{
		_baseEnumerable = baseEnumerable;
		_locker = new object();
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				foreach (var item in _cache.AsSpan())
				{
					seed = func.Eval(in seed, item);
				}

				return selector.Eval(in seed);
			}

			using var enumerable = _baseEnumerable.GetEnumerator();

			while (enumerable.MoveNext())
			{
				seed = func.Eval(in seed, enumerable.Current);
				_cache.Add(enumerable.Current);
			}

			_cacheInitialized = true;
			return selector.Eval(in seed);
		}
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				foreach (var item in _cache.AsSpan())
				{
					seed = @operator.Eval(in seed, item);
				}
			}
			else
			{
				using var enumerable = _baseEnumerable.GetEnumerator();

				while (enumerable.MoveNext())
				{
					seed = @operator.Eval(in seed, enumerable.Current);
					_cache.Add(enumerable.Current);
				}

				_cacheInitialized = true;
			}

			return seed;
		}
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				for (var i = 0; i < _cache.Count; i++)
				{
					if (!@operator.Eval(_cache[i]))
					{
						return false;
					}
				}
			}
			else
			{
				using var enumerable = _baseEnumerable.GetEnumerator();

				while (enumerable.MoveNext())
				{
					if (!@operator.Eval(enumerable.Current))
					{
						return false;
					}

					_cache.Add(enumerable.Current);
				}

				_cacheInitialized = true;
			}

			return true;
		}
	}

	public bool Any()
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				return _cache.Count > 0;
			}


			return _baseEnumerable.Any();
		}
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				for (var i = 0; i < _cache.Count; i++)
				{
					if (@operator.Eval(_cache[i]))
					{
						return true;
					}
				}
			}
			else
			{
				using var enumerable = _baseEnumerable.GetEnumerator();

				while (enumerable.MoveNext())
				{
					if (@operator.Eval(enumerable.Current))
					{
						return true;
					}

					_cache.Add(enumerable.Current);
				}

				_cacheInitialized = true;
			}

			return false;
		}
	}

	public IEnumerable<T> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				for (var i = 0; i < _cache.Count; i++)
				{
					if (comparer.Equals(_cache[i], item))
					{
						return true;
					}
				}
			}
			else
			{
				using var enumerable = _baseEnumerable.GetEnumerator();

				while (enumerable.MoveNext())
				{
					if (comparer.Equals(enumerable.Current, item))
					{
						return true;
					}

					_cache.Add(enumerable.Current);
				}

				_cacheInitialized = true;
			}

			return false;
		}
	}

	public bool Contains(in T item)
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				for (var i = 0; i < _cache.Count; i++)
				{
					if (EqualityComparer<T>.Default.Equals(_cache[i], item))
					{
						return true;
					}
				}
			}
			else
			{
				using var enumerable = _baseEnumerable.GetEnumerator();

				while (enumerable.MoveNext())
				{
					if (EqualityComparer<T>.Default.Equals(enumerable.Current, item))
					{
						return true;
					}

					_cache.Add(enumerable.Current);
				}

				_cacheInitialized = true;
			}

			return false;
		}
	}

	public int CopyTo(Span<T> data)
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				return _cache.CopyTo(data);
			}

			return _baseEnumerable.CopyTo(data);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				return TNumber.CreateChecked(_cache.Count);
			}

			using var enumerator = _baseEnumerable.GetEnumerator();

			var result = TNumber.Zero;

			while (enumerator.MoveNext())
			{
				_cache.Add(enumerator.Current);

				result++;
			}

			return result;
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

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T, bool>
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				var count = TNumber.Zero;

				for (var i = 0; i < _cache.Count; i++)
				{
					if (@operator.Eval(_cache[i]))
					{
						count++;
					}
				}

				return count;
			}

			return _baseEnumerable.Count<TCountOperator, TNumber>(@operator);
		}
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				var count = TNumber.Zero;

				for (var i = 0; i < _cache.Count; i++)
				{
					if (predicate(_cache[i]))
					{
						count++;
					}
				}

				return count;
			}

			return _baseEnumerable.Count<TNumber>(predicate);
		}
	}

	public int Count(Func<T, bool> predicate)
	{
		return Count<int>(predicate);
	}

	public long CountLong(Func<T, bool> predicate)
	{
		return Count<long>(predicate);
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				if (index > TIndex.CreateChecked(_cache.Count))
				{
					item = default;
					return false;
				}

				item = _cache[Int32.CreateChecked(index)];
				return true;
			}

			return _baseEnumerable.TryGetElementAt(index, out item);
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
			if (_cacheInitialized)
			{
				if (_cache.Count != 0)
				{
					item = _cache[0];
					return true;
				}

				item = default;
				return false;
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

			if (_cacheInitialized)
			{
				for (var i = 0; i < _cache.Count; i++)
				{
					Task.Factory.StartNew(x => @operator.Do((T)x), _cache[i], token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
				}
			}
			else
			{
				using var enumerator = _baseEnumerable.GetEnumerator();
				_cache = new PooledList<T>();

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);
					Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
				}

				_cacheInitialized = true;
			}

			schedulerPair.Complete();
			return schedulerPair.Completion;
		}
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				for (var i = 0; i < _cache.Count; i++)
				{
					@operator.Do(_cache[i]);
				}
			}
			else
			{
				using var enumerator = _baseEnumerable.GetEnumerator();

				while (enumerator.MoveNext())
				{
					_cache.Add(enumerator.Current);
				}

				_cacheInitialized = true;
			}
		}
	}

	public bool TryGetLast(out T item)
	{
		lock (_locker)
		{
			if (_cacheInitialized)
			{
				InitializeList();
			}

			if (_cache.Count == 0)
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
			if (_cacheInitialized)
			{
				switch (_cache.Count)
				{
					case 0:
						item = default!;
						return false;
					case 1:
						item = _cache[0];
						return true;
					default:
						throw new InvalidOperationException("Sequence contains more than one element");
				}
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
			if (!_cacheInitialized)
			{
				InitializeList();
			}

			return _cache.ToArray();
		}
	}

	public T[] ToArray(out int length)
	{
		lock (_locker)
		{
			if (!_cacheInitialized)
			{
				InitializeList();
			}

			length = _cache.Count;
			return _cache.ToArray();
		}
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = null)
	{
		lock (_locker)
		{
			if (!_cacheInitialized)
			{
				InitializeList();
			}

			var hashset = new HashSet<T>(_cache.Count, comparer);

			for (var i = 0; i < _cache.Count; i++)
			{
				hashset.Add(_cache[i]);
			}

			return hashset;
		}
	}

	public List<T> ToList()
	{
		lock (_locker)
		{
			if (!_cacheInitialized)
			{
				InitializeList();
			}

			var list = new List<T>(_cache.Count);

			for (var i = 0; i < _cache.Count; i++)
			{
				list.Add(_cache[i]);
			}

			return list;
		}
	}

	public PooledList<T> ToPooledList()
	{
		lock (_locker)
		{
			if (!_cacheInitialized)
			{
				InitializeList();
			}

			var list = new PooledList<T>(_cache.Count);

			_cache.CopyTo(list.AsSpan());

			return list;
		}
	}

	public PooledQueue<T> ToPooledQueue()
	{
		lock (_locker)
		{
			if (!_cacheInitialized)
			{
				InitializeList();
			}

			var queue = new PooledQueue<T>(_cache.Count);

			for (var i = 0; i < _cache.Count; i++)
			{
				queue.Enqueue(_cache[i]);
			}

			return queue;
		}
	}

	public PooledStack<T> ToPooledStack()
	{
		lock (_locker)
		{
			if (!_cacheInitialized)
			{
				InitializeList();
			}

			var stack = new PooledStack<T>(_cache.Count)
			{
				Count = _cache.Count,
			};

			_cache.CopyTo(stack.AsSpan());

			return stack;
		}
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		lock (_locker)
		{
			if (!_cacheInitialized)
			{
				InitializeList();
			}

			var set = new PooledSet<T, TComparer>(_cache.Count, comparer);

			for (var i = 0; i < _cache.Count; i++)
			{
				set.Add(_cache[i]);
			}

			return set;
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
			if (_cacheInitialized)
			{
				span = _cache.AsSpan();
				return true;
			}

			return _baseEnumerable.TryGetSpan(out span);
		}
	}

	public MemoizeEnumerator<T, TBaseEnumerator> GetEnumerator()
	{
		return new MemoizeEnumerator<T, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _locker, ref _cache, _cacheInitialized);
	}

	private void InitializeList()
	{
		if (_baseEnumerable.TryGetNonEnumeratedCount(out var count))
		{
			_cache.EnsureCapacity(count);
			_baseEnumerable.CopyTo(_cache.AsSpan());
		}
		else
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				_cache.Add(enumerator.Current);
			}
		}

		_cacheInitialized = true;
	}

	public void Dispose()
	{
		_cache.Dispose();
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}