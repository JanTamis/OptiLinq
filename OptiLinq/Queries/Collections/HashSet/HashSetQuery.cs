using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

// ReSharper disable GenericEnumeratorNotDisposed
public partial struct HashSetQuery<T> : IOptiQuery<T, HashSetEnumerator<T>>
{
	private readonly HashSet<T> _set;

	internal HashSetQuery(HashSet<T> set)
	{
		_set = set;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			seed = func.Eval(seed, enumerator.Current);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			seed = @operator.Eval(seed, enumerator.Current);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
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
		return _set.Count != 0;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		var enumerator = _set.GetEnumerator();

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
		return _set;
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (comparer.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(T item)
	{
		var enumerator = _set.GetEnumerator();

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
		var enumerator = _set.GetEnumerator();
		var i = 0;

		while (enumerator.MoveNext())
		{
			data[i++] = enumerator.Current;
		}

		return i;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return TNumber.CreateChecked(_set.Count);
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
			var enumerator = _set.GetEnumerator();

			while (enumerator.MoveNext())
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

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out T item)
	{
		var enumerator = _set.GetEnumerator();

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

		throw new InvalidOperationException("Sequence was empty");
	}

	public T FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			Task.Factory.StartNew(x => @operator.Do(x is null ? default : (T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		var enumerator = _set.GetEnumerator();

		while (enumerator.MoveNext())
		{
			@operator.Do(enumerator.Current);
		}
	}

	public bool TryGetLast(out T item)
	{
		var enumerator = _set.GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;

			while (enumerator.MoveNext())
			{
				item = enumerator.Current;
			}

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
		return EnumerableHelper.Max<T, HashSetEnumerator<T>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, HashSetEnumerator<T>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		var enumerator = _set.GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;

			if (!enumerator.MoveNext())
			{
				return true;
			}
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

		throw new InvalidOperationException("Sequence was empty or contained more than one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		var array = new T[_set.Count];

		_set.CopyTo(array, 0);

		return array;
	}

	public T[] ToArray(out int length)
	{
		length = _set.Count;
		return ToArray();
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		return new HashSet<T>(_set, comparer);
	}

	public List<T> ToList()
	{
		var list = new List<T>(_set.Count);
		var span = CollectionsMarshal.AsSpan(list);
		var enumerator = _set.GetEnumerator();

		for (var i = 0; i < span.Length && enumerator.MoveNext(); i++)
		{
			span[i] = enumerator.Current;
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = _set.Count;
		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public HashSetEnumerator<T> GetEnumerator()
	{
		return new HashSetEnumerator<T>(_set.GetEnumerator());
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();
}