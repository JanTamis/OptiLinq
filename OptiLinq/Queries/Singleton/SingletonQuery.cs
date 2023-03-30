using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SingletonQuery<T> : IOptiQuery<T, SingletonEnumerator<T>>
{
	private readonly T _element;

	internal SingletonQuery(in T element)
	{
		_element = element;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		return selector.Eval(func.Eval(in seed, in _element));
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		return @operator.Eval(in seed, in _element);
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		return @operator.Eval(in _element);
	}

	public bool Any()
	{
		return true;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		return @operator.Eval(in _element);
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
		if (!data.IsEmpty)
		{
			data[0] = _element;
			return 1;
		}

		return 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return TNumber.One;
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
		return @operator.Eval(in _element)
			? TNumber.One
			: TNumber.Zero;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		return predicate(_element)
			? TNumber.One
			: TNumber.Zero;
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
		if (TIndex.IsZero(index))
		{
			item = _element;
			return true;
		}

		item = default!;
		return false;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TIndex.IsZero(index))
		{
			return _element;
		}

		throw new ArgumentOutOfRangeException(nameof(index));
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TIndex.IsZero(index))
		{
			return _element;
		}

		return default!;
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
		return Task.Factory.StartNew(x => @operator.Do((T)x), _element, token);
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		@operator.Do(in _element);
	}

	public bool TryGetLast(out T item)
	{
		item = _element;
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
		item = _element;
		return true;
	}

	public T Single()
	{
		return _element;
	}

	public T SingleOrDefault()
	{
		return _element;
	}

	public T[] ToArray()
	{
		return new[] { _element };
	}

	public T[] ToArray(out int length)
	{
		length = 1;
		return ToArray();
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
		return new List<T>()
		{
			_element,
		};
	}

	public PooledList<T> ToPooledList()
	{
		var list = new PooledList<T>(1);

		list.Add(_element);

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = new PooledQueue<T>(1);

		queue.Enqueue(_element);

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = new PooledStack<T>(1);

		stack.Push(_element);

		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		var set = new PooledSet<T, TComparer>(1, comparer);

		set.Add(_element);

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 1;
		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public SingletonEnumerator<T> GetEnumerator()
	{
		return new SingletonEnumerator<T>(_element);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}