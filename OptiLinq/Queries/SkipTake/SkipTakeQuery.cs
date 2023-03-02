using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TSkipCount : IBinaryInteger<TSkipCount>
	where TTakeCount : IBinaryInteger<TTakeCount>
{
	private TBaseQuery _baseEnumerable;
	private readonly TSkipCount _skipCount;
	private readonly TTakeCount _takeCount;

	internal SkipTakeQuery(ref TBaseQuery baseEnumerable, TSkipCount skipCount, TTakeCount takeCount)
	{
		_baseEnumerable = baseEnumerable;
		_skipCount = skipCount;
		_takeCount = takeCount;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			seed = func.Eval(seed, enumerator.Current);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			seed = @operator.Eval(seed, enumerator.Current);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
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
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		return enumerator.MoveNext();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
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
		return new QueryAsEnumerable<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(this);
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			if (comparer.Equals(item, enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(T item)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			if (EqualityComparer<T>.Default.Equals(item, enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var j = TSkipCount.Zero; j < _skipCount && enumerator.MoveNext(); j++)
		{
		}

		var i = 0;
		var length = Math.Min(data.Length, Int32.CreateSaturating(_takeCount));

		for (; i < length && enumerator.MoveNext(); i++)
		{
			data[i] = enumerator.Current;
		}

		return i;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			count++;
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
		if (index >= TIndex.Zero)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
			{
			}

			for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
			{
				if (index == TIndex.Zero)
				{
					item = enumerator.Current;
					return true;
				}

				index--;
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
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;
			return true;
		}

		item = default!;
		return false;
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
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			@operator.Do(enumerator.Current);
		}
	}

	public bool TryGetLast(out T item)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();
		var isValid = false;

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		if (enumerator.MoveNext())
		{
			isValid = true;
		}

		if (!isValid)
		{
			item = default!;
			return false;
		}

		item = enumerator.Current;

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			item = enumerator.Current;
		}

		return true;
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
		return EnumerableHelper.Max<T, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		item = default!;

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;
		}

		if (!enumerator.MoveNext())
		{
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

		throw new Exception("Sequence does not contain exactly one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		LargeArrayBuilder<T> builder = new();
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			builder.Add(enumerator.Current);
		}

		return builder.ToArray();
	}

	public T[] ToArray(out int length)
	{
		if (_baseEnumerable.TryGetNonEnumeratedCount(out var count))
		{
			using var enumerator = _baseEnumerable.GetEnumerator();
			length = 0;

			for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
			{
				length++;
			}

			var array = new T[Int32.Min(count, Int32.CreateChecked(_takeCount))];

			for (var i = 0; i < array.Length && enumerator.MoveNext(); i++)
			{
				length++;
				array[i] = enumerator.Current;
			}

			return array;
		}

		return EnumerableHelper.ToArray<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(this, out length);
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var set = new HashSet<T>(comparer);

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public List<T> ToList()
	{
		var list = new List<T>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		if (_baseEnumerable.TryGetSpan(out var baseSpan))
		{
			var skipCount = Int32.CreateChecked(_skipCount);
			var takeCount = Int32.CreateChecked(_takeCount);

			span = baseSpan.Slice(skipCount, Int32.Min(baseSpan.Length - skipCount, takeCount));
			return true;
		}

		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator> GetEnumerator()
	{
		return new SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _skipCount, _takeCount);
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator()
	{
		return GetEnumerator();
	}
}