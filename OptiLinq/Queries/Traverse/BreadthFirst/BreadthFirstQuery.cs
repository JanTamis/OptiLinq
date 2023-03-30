using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq.Queries.Traverse.BreadthFirst;

public struct BreadthFirstQuery<T, TSelector, TSelectorEnumerable> : IOptiQuery<T, BreadthFirstEnumerator<T, TSelector, TSelectorEnumerable>>
	where TSelector : struct, IFunction<T, TSelectorEnumerable>
	where TSelectorEnumerable : IEnumerable<T>
{
	private TSelector _selector;
	private readonly T _root;

	public BreadthFirstQuery(TSelector selector, T root)
	{
		_selector = selector;
		_root = root;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var queue = GetQueue();

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			seed = func.Eval(in seed, in current);

			foreach (var item in _selector.Eval(in current))
			{
				queue.Enqueue(item);
			}
		}

		return selector.Eval(in seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var queue = GetQueue();

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			seed = @operator.Eval(in seed, in current);

			foreach (var item in _selector.Eval(in current))
			{
				queue.Enqueue(item);
			}
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var queue = GetQueue();

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			if (!@operator.Eval(current))
			{
				return false;
			}

			foreach (var item in _selector.Eval(current))
			{
				queue.Enqueue(item);
			}
		}

		return true;
	}

	public bool Any()
	{
		return true;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var queue = GetQueue();

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			if (@operator.Eval(current))
			{
				return true;
			}

			foreach (var item in _selector.Eval(current))
			{
				queue.Enqueue(item);
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
		using var queue = GetQueue();

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			if (comparer.Equals(current, item))
			{
				return true;
			}

			foreach (var child in _selector.Eval(current))
			{
				queue.Enqueue(child);
			}
		}

		return false;
	}

	public bool Contains(in T item)
	{
		using var queue = GetQueue();

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			if (EqualityComparer<T>.Default.Equals(current, item))
			{
				return true;
			}

			foreach (var child in _selector.Eval(current))
			{
				queue.Enqueue(child);
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		using var queue = GetQueue();

		var i = 0;

		while (queue.Count != 0 && i < data.Length)
		{
			var current = queue.Dequeue();

			data[i++] = current;

			foreach (var child in _selector.Eval(current))
			{
				queue.Enqueue(child);
			}
		}

		return i;
	}

	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		using var queue = GetQueue();

		var count = TNumber.Zero;

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			count++;

			foreach (var child in _selector.Eval(current))
			{
				queue.Enqueue(child);
			}
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

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TCountOperator : struct, IFunction<T, bool> where TNumber : INumberBase<TNumber>
	{
		using var queue = GetQueue();

		var count = TNumber.Zero;

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			if (@operator.Eval(current))
			{
				count++;
			}

			foreach (var child in _selector.Eval(current))
			{
				queue.Enqueue(child);
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		using var queue = GetQueue();

		var count = TNumber.Zero;

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			if (predicate(current))
			{
				count++;
			}

			foreach (var child in _selector.Eval(current))
			{
				queue.Enqueue(child);
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
		using var queue = GetQueue();

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			if (TIndex.IsZero(index))
			{
				item = current;

				return true;
			}

			index--;

			foreach (var child in _selector.Eval(current))
			{
				queue.Enqueue(child);
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
		item = _root;
		return true;
	}

	public T First()
	{
		return _root;
	}

	public T FirstOrDefault()
	{
		return _root;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		var queue = GetQueue();

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			Task.Factory.StartNew(x => @operator.Do((T)x), current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);

			foreach (var child in _selector.Eval(current))
			{
				queue.Enqueue(child);
			}
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var queue = GetQueue();

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			@operator.Do(current);

			foreach (var child in _selector.Eval(current))
			{
				queue.Enqueue(child);
			}
		}
	}

	public bool TryGetLast(out T item)
	{
		using var queue = GetQueue();
		item = default!;

		while (queue.Count != 0)
		{
			item = queue.Dequeue();

			foreach (var child in _selector.Eval(item))
			{
				queue.Enqueue(child);
			}
		}

		return true;
	}

	public T Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException();
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, BreadthFirstEnumerator<T, TSelector, TSelectorEnumerable>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, BreadthFirstEnumerator<T, TSelector, TSelectorEnumerable>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		var enumerable = _selector.Eval(_root);

		if (enumerable.Any())
		{
			item = default!;
			return false;
		}

		item = _root;
		return true;
	}

	public T Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException();
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		using var list = new PooledList<T>();
		using var queue = GetQueue();

		while (queue.Count != 0)
		{
			var current = queue.Dequeue();

			list.Add(in current);

			foreach (var child in _selector.Eval(in current))
			{
				queue.Enqueue(child);
			}
		}

		return list.ToArray();
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		throw new NotImplementedException();
	}

	public List<T> ToList()
	{
		throw new NotImplementedException();
	}

	public PooledList<T> ToPooledList()
	{
		throw new NotImplementedException();
	}

	public PooledQueue<T> ToPooledQueue()
	{
		throw new NotImplementedException();
	}

	public PooledStack<T> ToPooledStack()
	{
		throw new NotImplementedException();
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		throw new NotImplementedException();
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		throw new NotImplementedException();
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		throw new NotImplementedException();
	}

	public BreadthFirstEnumerator<T, TSelector, TSelectorEnumerable> GetEnumerator()
	{
		return new BreadthFirstEnumerator<T, TSelector, TSelectorEnumerable>(_selector, _root);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private PooledQueue<T> GetQueue()
	{
		var queue = new PooledQueue<T>();
		queue.Enqueue(_root);

		return queue;
	}
}