using System.Numerics;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>
	where TOperator : struct, IFunction<T, bool>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseQuery _baseQuery;
	private TOperator _operator;

	private int _count = -1;

	internal TakeWhileQuery(TBaseQuery baseQuery, TOperator @operator)
	{
		_baseQuery = baseQuery;
		_operator = @operator;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		var count = 0;
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			seed = func.Eval(seed, enumerator.Current);
			count++;
		}

		_count = count;

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		var count = 0;
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			seed = @operator.Eval(seed, enumerator.Current);
			count++;
		}

		_count = count;

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
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
		using var enumerator = _baseQuery.GetEnumerator();

		return enumerator.MoveNext() && _operator.Eval(enumerator.Current);
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
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
		return new QueryAsEnumerable<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>(this);
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
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
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
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
		using var enumerator = _baseQuery.GetEnumerator();

		var i = 0;

		while (i < data.Length && enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			data[i++] = enumerator.Current;
		}

		return i;
	}

	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		if (TryGetNonEnumeratedCount(out var count))
		{
			return TNumber.CreateChecked(count);
		}

		using var enumerator = _baseQuery.GetEnumerator();

		var i = TNumber.Zero;

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			i++;
		}

		return i;
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
		if (TIndex.IsPositive(index))
		{
			using var enumerator = _baseQuery.GetEnumerator();

			while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
			{
				if (TIndex.IsZero(index))
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

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out T item)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		if (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
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

		throw new InvalidOperationException("Sequence was empty");
	}

	public T FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var count = 0;
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
			count++;
		}

		_count = count;

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		var count = 0;
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			@operator.Do(enumerator.Current);
			count++;
		}

		_count = count;
	}

	public bool TryGetLast(out T item)
	{
		var count = 0;
		using var enumerator = _baseQuery.GetEnumerator();

		if (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			item = enumerator.Current;

			while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
			{
				item = enumerator.Current;
				count++;
			}

			_count = count + 1;
			return true;
		}

		item = default!;
		return false;
	}

	public T Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence was empty");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		var count = 0;
		using var enumerator = _baseQuery.GetEnumerator();

		if (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			var max = enumerator.Current;

			while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
			{
				if (Comparer<T>.Default.Compare(max, enumerator.Current) < 0)
				{
					max = enumerator.Current;
				}

				count++;
			}

			_count = count + 1;
			return max;
		}

		throw new InvalidOperationException("Sequence was empty");
	}

	public T Min()
	{
		var count = 0;
		using var enumerator = _baseQuery.GetEnumerator();

		if (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			var min = enumerator.Current;

			while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
			{
				if (Comparer<T>.Default.Compare(min, enumerator.Current) > 0)
				{
					min = enumerator.Current;
				}

				count++;
			}

			_count = count + 1;
			return min;
		}

		throw new InvalidOperationException("Sequence was empty");
	}

	public bool TryGetSingle(out T item)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		if (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			var single = enumerator.Current;

			if (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
			{
				item = default!;
				return false;
			}

			_count = 1;

			item = single;
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

		throw new InvalidOperationException("Sequence contained more than one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		if (TryGetNonEnumeratedCount(out var count))
		{
			var array = GC.AllocateUninitializedArray<T>(count);

			CopyTo(array);

			return array;
		}

		return EnumerableHelper.ToArray<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>(this, out _);
	}

	public T[] ToArray(out int length)
	{
		if (TryGetNonEnumeratedCount(out var count))
		{
			var array = GC.AllocateUninitializedArray<T>(count);

			length = CopyTo(array);

			return array;
		}

		return EnumerableHelper.ToArray<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>(this, out length);
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var set = new HashSet<T>();
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public List<T> ToList()
	{
		List<T> list;

		if (TryGetNonEnumeratedCount(out var count))
		{
			list = new List<T>(count);

			CopyTo(CollectionsMarshal.AsSpan(list));

			return list;
		}

		list = new List<T>();

		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext() && _operator.Eval(enumerator.Current))
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_count >= 0)
		{
			length = _count;
			return true;
		}

		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		if (_baseQuery.TryGetSpan(out span))
		{
			var index = 0;

			while (index < span.Length && _operator.Eval(span[index]))
			{
				index++;
			}

			span = span.Slice(0, index);
		}

		return false;
	}

	public TakeWhileEnumerator<T, TOperator, TBaseEnumerator> GetEnumerator()
	{
		return new TakeWhileEnumerator<T, TOperator, TBaseEnumerator>(_baseQuery.GetEnumerator(), _operator);
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();
}