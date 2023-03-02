using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery> : IOptiQuery<T, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>>
	where TFirstQuery : struct, IOptiQuery<T, TFirstEnumerator>
	where TSecondQuery : struct, IOptiQuery<T>
	where TFirstEnumerator : struct, IOptiEnumerator<T>
{
	private TFirstQuery _firstQuery;
	private TSecondQuery _secondQuery;

	internal ConcatQuery(ref TFirstQuery firstQuery, ref TSecondQuery secondQuery)
	{
		_firstQuery = firstQuery;
		_secondQuery = secondQuery;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		return _secondQuery.Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(func, selector, _firstQuery.Aggregate(func, seed));
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		return _secondQuery.Aggregate(@operator, _firstQuery.Aggregate(@operator, seed));
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		return _firstQuery.All(@operator) && _secondQuery.All(@operator);
	}

	public bool Any()
	{
		return _firstQuery.Any() || _secondQuery.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		return _firstQuery.Any(@operator) || _secondQuery.Any(@operator);
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>>(this);
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return _firstQuery.Contains(item, comparer) || _secondQuery.Contains(item, comparer);
	}

	public bool Contains(T item)
	{
		return _firstQuery.Contains(item) || _secondQuery.Contains(item);
	}

	public int CopyTo(Span<T> data)
	{
		var firstCount = _firstQuery.CopyTo(data);
		return firstCount + _secondQuery.CopyTo(data.Slice(firstCount));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return _firstQuery.Count<TNumber>() + _secondQuery.Count<TNumber>();
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
		return EnumerableHelper.TryGetElementAt(GetEnumerator(), index, out item);
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
		return _firstQuery.TryGetFirst(out item) || _secondQuery.TryGetFirst(out item);
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
		return Task.WhenAll(_firstQuery.ForAll(@operator, token), _secondQuery.ForAll(@operator, token));
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		_firstQuery.ForEach(@operator);
		_secondQuery.ForEach(@operator);
	}

	public bool TryGetLast(out T item)
	{
		return _secondQuery.TryGetLast(out item) || _firstQuery.TryGetLast(out item);
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
		if (typeof(TFirstQuery) == typeof(TSecondQuery))
		{
			_firstQuery.Max();
		}

		var firstMax = _firstQuery.Max();
		var secondMax = _secondQuery.Max();

		return Comparer<T>.Default.Compare(firstMax, secondMax) > 0
			? firstMax
			: secondMax;
	}

	public T Min()
	{
		if (typeof(TFirstQuery) == typeof(TSecondQuery))
		{
			_firstQuery.Min();
		}

		var firstMin = _firstQuery.Min();
		var secondMin = _secondQuery.Min();

		return Comparer<T>.Default.Compare(firstMin, secondMin) < 0
			? firstMin
			: secondMin;
	}

	public bool TryGetSingle(out T item)
	{
		using var enumerator = GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;

			return !enumerator.MoveNext();
		}

		item = default!;
		return false;
	}

	public T Single()
	{
		if (TryGetFirst(out var result))
		{
			return result;
		}

		throw new InvalidOperationException("Sequence does not contain exactly one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var result);
		return result;
	}

	public T[] ToArray()
	{
		if (typeof(TFirstQuery) == typeof(TSecondQuery))
		{
			if (_firstQuery.TryGetNonEnumeratedCount(out var count))
			{
				var array = new T[count * 2];
				_firstQuery.CopyTo(array);
				array.AsSpan(0, count).CopyTo(array.AsSpan(count, count));

				return array;
			}
			else
			{
				var builder = new LargeArrayBuilder<T>();

				using var enumerator = _firstQuery.GetEnumerator();

				while (enumerator.MoveNext())
				{
					builder.Add(enumerator.Current);
				}

				var array = new T[builder.Count * 2];
				builder.CopyTo(array, 0, builder.Count);

				array.AsSpan(0, builder.Count)
					.CopyTo(array.AsSpan(builder.Count, builder.Count));

				return array;
			}
		}

		{
			var firstCount = 0;
			var secondCount = 0;

			if (_firstQuery.TryGetNonEnumeratedCount(out firstCount) && _secondQuery.TryGetNonEnumeratedCount(out secondCount))
			{
				var array = new T[firstCount + secondCount];
				_firstQuery.CopyTo(array);
				_secondQuery.CopyTo(array.AsSpan(firstCount));
				return array;
			}

			var builder = new LargeArrayBuilder<T>(firstCount + secondCount);

			using var firstEnumerator = _firstQuery.GetEnumerator();
			using var secondEnumerator = _secondQuery.GetEnumerator();

			while (firstEnumerator.MoveNext())
			{
				builder.Add(firstEnumerator.Current);
			}

			while (secondEnumerator.MoveNext())
			{
				builder.Add(secondEnumerator.Current);
			}

			return builder.ToArray();
		}
	}

	public T[] ToArray(out int length)
	{
		var array = ToArray();
		length = array.Length;

		return array;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		comparer ??= EqualityComparer<T>.Default;

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();

		_firstQuery.TryGetNonEnumeratedCount(out var firstCount);
		_secondQuery.TryGetNonEnumeratedCount(out var secondCount);

		var set = new HashSet<T>(firstCount + secondCount, comparer);

		while (firstEnumerator.MoveNext())
		{
			set.Add(firstEnumerator.Current);
		}

		while (secondEnumerator.MoveNext())
		{
			set.Add(secondEnumerator.Current);
		}

		return set;
	}

	public List<T> ToList()
	{
		var firstCount = 0;
		var secondCount = 0;

		List<T> list;

		if (_firstQuery.TryGetNonEnumeratedCount(out firstCount) && _secondQuery.TryGetNonEnumeratedCount(out secondCount))
		{
			list = new List<T>(firstCount + secondCount);
			var span = CollectionsMarshal.AsSpan(list);

			_firstQuery.CopyTo(span);
			_secondQuery.CopyTo(span.Slice(firstCount));
		}
		else
		{
			list = new List<T>(firstCount + secondCount);

			using var firstEnumerator = _firstQuery.GetEnumerator();
			using var secondEnumerator = _secondQuery.GetEnumerator();

			while (firstEnumerator.MoveNext())
			{
				list.Add(firstEnumerator.Current);
			}

			while (secondEnumerator.MoveNext())
			{
				list.Add(secondEnumerator.Current);
			}
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_firstQuery.TryGetNonEnumeratedCount(out var firstCount) && _secondQuery.TryGetNonEnumeratedCount(out var secondCount))
		{
			length = firstCount + secondCount;
			return true;
		}

		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>> GetEnumerator()
	{
		return new ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>(_firstQuery.GetEnumerator(), _secondQuery.GetEnumerator());
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();
}