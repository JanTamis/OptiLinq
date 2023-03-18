using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RepeatQuery<T> : IOptiQuery<T, RepeatEnumerator<T>>
{
	private readonly T _element;

	internal RepeatQuery(T element)
	{
		_element = element;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		return selector.Eval(func.Eval(seed, _element));
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		return @operator.Eval(_element);
	}

	public bool Any()
	{
		return true;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		return @operator.Eval(_element);
	}

	public IEnumerable<T> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return comparer.Equals(item, _element);
	}

	public bool Contains(in T item)
	{
		return EqualityComparer<T>.Default.Equals(item, _element);
	}

	public int CopyTo(Span<T> data)
	{
		data.Fill(_element);

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
		if (@operator.Eval(in _element))
		{
			return TNumber.CreateSaturating(UInt128.MaxValue);
		}

		return TNumber.Zero;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		if (predicate(_element))
		{
			return TNumber.CreateSaturating(UInt128.MaxValue);
		}

		return TNumber.Zero;
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
		item = _element;
		return true;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		return _element;
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		return _element;
	}

	public bool TryGetFirst(out T item)
	{
		item = _element;
		return true;
	}

	public T First()
	{
		return _element;
	}

	public T FirstOrDefault()
	{
		return _element;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		return Task.CompletedTask;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		while (true)
		{
			@operator.Do(in _element);
		}
	}

	public bool TryGetLast(out T item)
	{
		item = default!;
		return true;
	}

	public T Last()
	{
		return _element;
	}

	public T LastOrDefault()
	{
		return _element;
	}

	public T Max()
	{
		return _element;
	}

	public T Min()
	{
		return _element;
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
		return default!;
	}

	public T[] ToArray()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public T[] ToArray(out int length)
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		return new HashSet<T>(1, comparer)
		{
			_element,
		};
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
		var set = new PooledSet<T, TComparer>(1, comparer);
		set.Add(_element);

		return set;
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

	public RepeatEnumerator<T> GetEnumerator()
	{
		return new RepeatEnumerator<T>(_element);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}