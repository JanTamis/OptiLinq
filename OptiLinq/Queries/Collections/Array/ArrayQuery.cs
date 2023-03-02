using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ArrayQuery<T> : IOptiQuery<T, ArrayEnumerator<T>>
{
	private readonly T[] _array;

	internal ArrayQuery(T[] list)
	{
		_array = list;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		ref var first = ref MemoryMarshal.GetArrayDataReference(_array);

		for (var i = 0; i < _array.Length; i++)
		{
			seed = func.Eval(seed, Unsafe.Add(ref first, i));
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		ref var first = ref MemoryMarshal.GetArrayDataReference(_array);

		for (var i = 0; i < _array.Length; i++)
		{
			seed = @operator.Eval(seed, Unsafe.Add(ref first, i));
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		for (var i = 0; i < _array.Length; i++)
		{
			if (!@operator.Eval(_array[i]))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		return _array.Length != 0;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		for (var i = 0; i < _array.Length; i++)
		{
			if (@operator.Eval(_array[i]))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return _array;
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		for (var i = 0; i < _array.Length; i++)
		{
			if (comparer.Equals(_array[i], item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(T item)
	{
		return _array.Contains(item);
	}

	public int CopyTo(Span<T> data)
	{
		var length = Math.Min(_array.Length, data.Length);

		_array.AsSpan(0, length).CopyTo(data);

		return length;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return TNumber.CreateChecked(_array.Length);
	}

	public readonly int Count()
	{
		return _array.Length;
	}

	public long LongCount()
	{
		return _array.LongLength;
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		if (TIndex.IsPositive(index))
		{
			var intIndex = Int32.CreateChecked(index);

			if (intIndex < _array.Length)
			{
				item = _array[intIndex];
				return true;
			}
		}

		item = default;
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
		TryGetElementAt(index, out var result);
		return result;
	}

	public bool TryGetFirst(out T item)
	{
		if (_array.Length != 0)
		{
			item = _array[0];
			return true;
		}

		item = default!;
		return false;
	}

	public T First()
	{
		if (TryGetFirst(out var first))
		{
			return first;
		}

		throw new Exception("Sequence doesn't contain a element");
	}

	public T FirstOrDefault()
	{
		if (_array.Length == 0)
		{
			return default;
		}

		return _array[0];
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		for (var i = 0; i < _array.Length; i++)
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), _array[i], token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		for (var i = 0; i < _array.Length; i++)
		{
			@operator.Do(_array[i]);
		}
	}

	public bool TryGetLast(out T item)
	{
		if (_array.Length != 0)
		{
			item = _array[^1];
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

		throw new Exception("Sequence doesn't contain a element");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, ArrayEnumerator<T>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, ArrayEnumerator<T>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		if (_array.Length == 1)
		{
			item = _array[0];
			return true;
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

		throw new Exception("Sequence doesn't contain one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		var array = new T[_array.Length];

		_array.CopyTo(array, 0);

		return array;
	}

	public T[] ToArray(out int length)
	{
		length = _array.Length;
		return ToArray();
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var set = new HashSet<T>(_array.Length, comparer);

		for (var i = 0; i < _array.Length; i++)
		{
			set.Add(_array[i]);
		}

		return set;
	}

	public List<T> ToList()
	{
		var list = new List<T>(_array.Length);
		var span = CollectionsMarshal.AsSpan(list);

		_array.AsSpan().CopyTo(span);

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = _array.Length;
		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = _array;
		return true;
	}

	public ArrayEnumerator<T> GetEnumerator()
	{
		return new ArrayEnumerator<T>(_array);
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();
}