using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;
using Exception = System.Exception;

namespace OptiLinq;

public partial struct RandomQuery : IOptiQuery<int, RandomEnumerator>
{
	private readonly int? _seed;

	public RandomQuery(int? seed)
	{
		_seed = seed;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, int, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, int, TAccumulate>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<int, bool>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public bool Any()
	{
		return true;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<int, bool>
	{
		var rng = GetRandom();

		while (true)
		{
			if (@operator.Eval(rng.Next()))
			{
				return true;
			}
		}
	}

	public IEnumerable<int> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in int item, TComparer comparer) where TComparer : IEqualityComparer<int>
	{
		var rng = GetRandom();

		while (true)
		{
			if (comparer.Equals(item, rng.Next()))
			{
				return true;
			}
		}
	}

	public bool Contains(in int item)
	{
		var rng = GetRandom();

		while (true)
		{
			if (rng.Next() == item)
			{
				return true;
			}
		}
	}

	public bool Contains(int item, IEqualityComparer<int>? comparer = default)
	{
		return true;
	}

	public int CopyTo(Span<int> data)
	{
		var rng = GetRandom();

		for (var i = 0; i < data.Length; i++)
		{
			data[i] = rng.Next();
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
		throw ThrowHelper.CreateInfiniteException();
	}

	public long LongCount()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<int, bool>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public TNumber Count<TNumber>(Func<int, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public int Count(Func<int, bool> predicate)
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public long CountLong(Func<int, bool> predicate)
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out int item) where TIndex : IBinaryInteger<TIndex>
	{
		item = default;

		if (TIndex.IsZero(index))
		{
			return false;
		}

		var rng = GetRandom();

		for (var i = TIndex.Zero; i < index; i++)
		{
			item = rng.Next();
		}

		return true;
	}

	public int ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		throw new IndexOutOfRangeException();
	}

	public int ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out int item)
	{
		item = GetRandom().Next();
		return true;
	}

	public int First()
	{
		return GetRandom().Next();
	}

	public int FirstOrDefault()
	{
		return GetRandom().Next();
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<int>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		var rng = GetRandom();

		while (true)
		{
			Task.Factory.StartNew(x => @operator.Do(Unsafe.Unbox<int>(x)), rng.Next(), token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<int>
	{
		var rng = GetRandom();

		while (true)
		{
			@operator.Do(rng.Next());
		}
	}

	public bool TryGetLast(out int item)
	{
		item = default;
		return false;
	}

	public int Last()
	{
		return Random.Shared.Next();
	}

	public int LastOrDefault()
	{
		return Random.Shared.Next();
	}

	public int Max()
	{
		return Int32.MaxValue;
	}

	public int Min()
	{
		return Int32.MinValue;
	}

	public bool TryGetSingle(out int item)
	{
		item = default;
		return false;
	}

	public int Single()
	{
		throw new Exception("Sequence contains more than one element");
	}

	public int SingleOrDefault()
	{
		return default;
	}

	public int[] ToArray()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public int[] ToArray(out int length)
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public HashSet<int> ToHashSet(IEqualityComparer<int>? comparer = default)
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public List<int> ToList()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public PooledList<int> ToPooledList()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public PooledQueue<int> ToPooledQueue()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public PooledStack<int> ToPooledStack()
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public PooledSet<int, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<int>
	{
		throw ThrowHelper.CreateInfiniteException();
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<int> span)
	{
		span = ReadOnlySpan<int>.Empty;
		return false;
	}

	public RandomEnumerator GetEnumerator()
	{
		return new RandomEnumerator(GetRandom());
	}

	IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private Random GetRandom()
	{
		if (_seed is null)
		{
			return Random.Shared;
		}

		return new Random(_seed.GetValueOrDefault());
	}
}