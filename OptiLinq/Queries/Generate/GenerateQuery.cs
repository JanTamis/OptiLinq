using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator> : IOptiQuery<T, GenerateEnumerator<T, TOperator>> where TOperator : struct, IFunction<T, T>
{
	private readonly T _parameter;
	private TOperator _operator;

	internal GenerateQuery(T parameter, TOperator @operator)
	{
		_parameter = parameter;
		_operator = @operator;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public bool Any()
	{
		return true;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
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

	public IEnumerable<T> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
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

	public bool Contains(in T item)
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (EqualityComparer<T>.Default.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		var current = _parameter;

		for (var i = 0; i < data.Length; i++)
		{
			data[i] = current = _operator.Eval(in current);
		}

		return data.Length;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		throw ThrowHelper.CreateInfiniteException();
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

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public int Count(Func<T, bool> predicate)
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public long CountLong(Func<T, bool> predicate)
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			var current = _parameter;

			while (index > TIndex.Zero)
			{
				current = _operator.Eval(current);
				index--;
			}

			item = current;
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

		throw ThrowHelper.CreateOutOfRangeException();
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out T item)
	{
		item = _parameter;
		return true;
	}

	public T First()
	{
		return _parameter;
	}

	public T FirstOrDefault()
	{
		return _parameter;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		return Task.CompletedTask;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		var current = _parameter;

		while (true)
		{
			@operator.Do(current = _operator.Eval(current));
		}
	}

	public bool TryGetLast(out T item)
	{
		item = default!;
		return false;
	}

	public T Last()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public T LastOrDefault()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public T Max()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public T Min()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public bool TryGetSingle(out T item)
	{
		item = default!;
		return false;
	}

	public T Single()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public T SingleOrDefault()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public T[] ToArray()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public List<T> ToList()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public PooledList<T> ToPooledList()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public PooledQueue<T> ToPooledQueue()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public PooledStack<T> ToPooledStack()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public GenerateEnumerator<T, TOperator> GetEnumerator()
	{
		return new GenerateEnumerator<T, TOperator>(_parameter, _operator);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}