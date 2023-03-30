using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct IListQuery<T> : IOptiQuery<T, IListEnumerator<T>>
{
	private readonly IList<T> _list;

	public IListQuery(IList<T> list)
	{
		_list = list;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		for (var i = 0; i < _list.Count; i++)
		{
			seed = func.Eval(in seed, _list[i]);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		for (var i = 0; i < _list.Count; i++)
		{
			seed = @operator.Eval(seed, _list[i]);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		for (var i = 0; i < _list.Count; i++)
		{
			if (!@operator.Eval(_list[i]))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		return _list.Count != 0;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		for (var i = 0; i < _list.Count; i++)
		{
			if (@operator.Eval(_list[i]))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return _list;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		for (var i = 0; i < _list.Count; i++)
		{
			if (comparer.Equals(_list[i], item))
			{
				return false;
			}
		}

		return true;
	}

	public bool Contains(in T item)
	{
		for (var i = 0; i < _list.Count; i++)
		{
			if (EqualityComparer<T>.Default.Equals(_list[i], item))
			{
				return false;
			}
		}

		return true;
	}

	public int CopyTo(Span<T> data)
	{
		var length = Math.Min(_list.Count, data.Length);

		switch (_list)
		{
			case T[] arr:
				arr.AsSpan(0, length).CopyTo(data);
				break;
			case List<T> list:
				CollectionsMarshal.AsSpan(list).Slice(0, length).CopyTo(data);
				break;
			default:
				for (var i = 0; i < length; i++)
				{
					data[i] = _list[i];
				}

				break;
		}

		return length;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return TNumber.CreateChecked(_list.Count);
	}

	public int Count()
	{
		return _list.Count;
	}

	public long LongCount()
	{
		return _list.Count;
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T, bool>
	{
		var count = TNumber.Zero;

		for (var i = 0; i < _list.Count; i++)
		{
			if (@operator.Eval(_list[i]))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		for (var i = 0; i < _list.Count; i++)
		{
			if (predicate(_list[i]))
			{
				count++;
			}
		}

		return count;
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
		if (index >= TIndex.Zero)
		{
			using var enumerator = GetEnumerator();

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
		if (_list.Count == 0)
		{
			item = default!;
			return false;
		}

		item = _list[0];
		return true;
	}

	public T First()
	{
		if (_list.Count == 0)
		{
			throw new Exception("Sequence doesn't contain a element");
		}

		return _list[0];
	}

	public T FirstOrDefault()
	{
		if (_list.Count == 0)
		{
			return default;
		}

		return _list[0];
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		var count = _list.Count;

		for (var i = 0; i < count; i++)
		{
			Task.Factory.StartNew(x => @operator.Do(x is null ? default : (T)x), _list[i], token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
			count = _list.Count;
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		var count = _list.Count;

		for (var i = 0; i < count; i++)
		{
			@operator.Do(_list[i]);
			count = _list.Count;
		}
	}

	public bool TryGetLast(out T item)
	{
		if (_list.Count == 0)
		{
			item = default!;
			return false;
		}

		item = _list[^1];
		return true;
	}

	public T Last()
	{
		if (_list.Count == 0)
		{
			throw new Exception("Sequence doesn't contain a element");
		}

		return _list[^1];
	}

	public T LastOrDefault()
	{
		if (_list.Count == 0)
		{
			return default;
		}

		return _list[^1];
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, IListEnumerator<T>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, IListEnumerator<T>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		if (_list.Count != 1)
		{
			item = default!;
			return false;
		}

		item = _list[0];
		return true;
	}

	public T Single()
	{
		if (_list.Count != 1)
		{
			throw new Exception("Sequence contains to much elements");
		}

		return _list[0];
	}

	public T SingleOrDefault()
	{
		if (_list.Count != 1)
		{
			return default;
		}

		return _list[0];
	}

	public T[] ToArray()
	{
		var array = new T[_list.Count];

		_list.CopyTo(array, 0);

		return array;
	}

	public T[] ToArray(out int length)
	{
		length = _list.Count;
		return ToArray();
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var set = new HashSet<T>(_list.Count, comparer);

		for (var i = 0; i < _list.Count; i++)
		{
			set.Add(_list[i]);
		}

		return set;
	}

	public List<T> ToList()
	{
		var list = new List<T>(_list.Count);
		var span = CollectionsMarshal.AsSpan(list);

		for (var i = 0; i < span.Length; i++)
		{
			span[i] = _list[i];
		}

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = new PooledList<T>(_list.Count)
		{
			Count = _list.Count,
		};

		if (TryGetSpan(out var span))
		{
			span.CopyTo(list.Items);
		}
		else
		{
			for (var i = 0; i < _list.Count; i++)
			{
				list.Items[i] = _list[i];
			}
		}


		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = new PooledQueue<T>(_list.Count);

		for (var i = 0; i < _list.Count; i++)
		{
			queue.Enqueue(_list[i]);
		}

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = new PooledStack<T>(_list.Count);

		for (var i = 0; i < _list.Count; i++)
		{
			stack.Push(_list[i]);
		}

		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		var set = new PooledSet<T, TComparer>(comparer);

		for (var i = 0; i < _list.Count; i++)
		{
			set.Add(_list[i]);
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = _list.Count;
		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		switch (_list)
		{
			case List<T> list:
				span = CollectionsMarshal.AsSpan(list);
				return true;
			case T[] array:
				span = array;
				return true;
			default:
				span = ReadOnlySpan<T>.Empty;
				return false;
		}
	}

	public IListEnumerator<T> GetEnumerator()
	{
		return new IListEnumerator<T>(_list);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}