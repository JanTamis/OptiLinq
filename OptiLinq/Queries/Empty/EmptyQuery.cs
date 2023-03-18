using System.Buffers;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct EmptyQuery<T> : IOptiQuery<T, EmptyEnumerator<T>>
{
	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		return false;
	}

	public bool Any()
	{
		return false;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return false;
	}

	public bool Contains(in T item)
	{
		return false;
	}

	public int CopyTo(Span<T> data)
	{
		return 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return TNumber.Zero;
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
		return TNumber.Zero;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		return TNumber.Zero;
	}

	public int Count(Func<T, bool> predicate)
	{
		return 0;
	}

	public long CountLong(Func<T, bool> predicate)
	{
		return 0L;
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		item = default!;
		return false;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		return default;
	}

	public bool TryGetFirst(out T item)
	{
		item = default!;
		return false;
	}

	public T First()
	{
		return default;
	}

	public T FirstOrDefault()
	{
		return default;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		return Task.CompletedTask;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
	}

	public bool TryGetLast(out T item)
	{
		item = default!;
		return false;
	}

	public T Last()
	{
		throw new Exception("Sequence doesn't contain elements");
	}

	public T LastOrDefault()
	{
		return default;
	}

	public T Max()
	{
		return default;
	}

	public T Min()
	{
		return default;
	}

	public bool TryGetSingle(out T item)
	{
		item = default!;
		return false;
	}

	public T Single()
	{
		throw new Exception("Sequence doesn't contain elements");
	}

	public T SingleOrDefault()
	{
		return default;
	}

	public T[] ToArray()
	{
		return Array.Empty<T>();
	}

	public T[] ToArray(out int length)
	{
		length = 0;
		return Array.Empty<T>();
	}

	public T[] ToArray(ArrayPool<T> pool, out int length)
	{
		length = 0;
		return pool.Rent(0);
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		return new HashSet<T>();
	}

	public List<T> ToList()
	{
		return new List<T>();
	}

	public PooledList<T> ToPooledList()
	{
		return new PooledList<T>();
	}

	public PooledQueue<T> ToPooledQueue()
	{
		return new PooledQueue<T>();
	}

	public PooledStack<T> ToPooledStack()
	{
		return new PooledStack<T>();
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new PooledSet<T, TComparer>(comparer);
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return true;
	}

	public EmptyEnumerator<T> GetEnumerator()
	{
		return new EmptyEnumerator<T>();
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}