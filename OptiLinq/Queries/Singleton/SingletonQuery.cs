using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SingletonQuery<T> : IOptiQuery<T, SingletonEnumerator<T>>
{
	private readonly T _element;

	internal SingletonQuery(in T element)
	{
		_element = element;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		return selector.Eval(func.Eval(seed, _element));
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		return @operator.Eval(seed, _element);
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
		return new QueryAsEnumerable<T, SingletonQuery<T>, SingletonEnumerator<T>>(this);
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return comparer.Equals(item, _element);
	}

	public bool Contains(T item)
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
		@operator.Do(_element);
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

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();
}