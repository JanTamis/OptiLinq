using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct OrderQuery<T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private TBaseQuery _baseEnumerable;
	private readonly IComparer<T> _comparer;

	private OrderedEnumerable<T, TBaseQuery, TBaseEnumerator> _orderedEnumerable;
	
	private int[]? _map;
	private Buffer<T, TBaseQuery, TBaseEnumerator> _buffer;

	internal OrderQuery(TBaseQuery baseEnumerable, IComparer<T>? comparer)
	{
		_baseEnumerable = baseEnumerable;
		_comparer = comparer ?? Comparer<T>.Default;
	}

	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		return _baseEnumerable.All<TAllOperator>();
	}

	public bool Any()
	{
		return _baseEnumerable.Any();
	}

	public IEnumerable<T> AsEnumerable()
	{
		throw new NotImplementedException();
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer = null)
	{
		return _baseEnumerable.Contains(item, comparer);
	}

	public int Count()
	{
		return _baseEnumerable.Count();
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			if (_map is not null && Int32.CreateChecked(index) < _map.Length)
			{
				return _buffer._items[_map[Int32.CreateChecked(index)]];
			}
			
			using var enumerator = GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (index == TIndex.Zero)
				{
					return enumerator.Current;
				}

				index--;
			}
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			if (_map is not null && Int32.CreateChecked(index) < _map.Length)
			{
				return _buffer._items[_map[Int32.CreateChecked(index)]];
			}

			using var enumerator = GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (index == TIndex.Zero)
				{
					return enumerator.Current;
				}

				index--;
			}
		}

		return default;
	}

	public T First()
	{
		if (_map is null)
		{
			using var enumerable = _baseEnumerable.GetEnumerator();

			if (enumerable.MoveNext())
			{
				var result = enumerable.Current;

				while (enumerable.MoveNext())
				{
					if (_comparer.Compare(result, enumerable.Current) < 0)
					{
						result = enumerable.Current;
					}
				}

				return result;
			}

			throw new Exception("Sequence doesn't contain elements");
		}

		if (_buffer._count is 0)
		{
			throw new Exception("Sequence doesn't contain elements");
		}

		return _buffer._items[_map[0]];
	}

	public T FirstOrDefault()
	{
		// if (_map is null)
		// {
		// 	using var enumerable = _baseEnumerable.GetEnumerator();
		//
		// 	if (enumerable.MoveNext())
		// 	{
		// 		var result = enumerable.Current;
		//
		// 		while (enumerable.MoveNext())
		// 		{
		// 			if (_comparer.Compare(result, enumerable.Current) > 0)
		// 			{
		// 				result = enumerable.Current;
		// 			}
		// 		}
		//
		// 		return result;
		// 	}
		//
		// 	return default!;
		// }

		if (_orderedEnumerable is null)
		{
			_orderedEnumerable = GenerateEnumerable();
		
			_buffer = new Buffer<T, TBaseQuery, TBaseEnumerator>(_baseEnumerable);
			_map = _orderedEnumerable.GetEnumerableSorter(null).Sort(_buffer._items, _buffer._count);
		}

		return _buffer._items[_map![0]];
	}

	public T Last()
	{
		if (_map is null)
		{
			using var enumerable = _baseEnumerable.GetEnumerator();

			if (enumerable.MoveNext())
			{
				var result = enumerable.Current;

				while (enumerable.MoveNext())
				{
					if (_comparer.Compare(result, enumerable.Current) > 0)
					{
						result = enumerable.Current;
					}
				}

				return result;
			}

			throw new Exception("Sequence doesn't contain elements");
		}

		if (_buffer._count is 0)
		{
			throw new Exception("Sequence doesn't contain elements");
		}

		return _buffer._items[_map[_buffer._count - 1]];
	}

	public T LastOrDefault()
	{
		if (_map is null)
		{
			using var enumerable = _baseEnumerable.GetEnumerator();

			if (enumerable.MoveNext())
			{
				var result = enumerable.Current;
				
				while (enumerable.MoveNext())
				{
					if (_comparer.Compare(result, enumerable.Current) > 0)
					{
						result = enumerable.Current;
					}
				}
				
				return result;
			}

			return default!;
		}

		return _buffer._items[_map[_buffer._count - 1]];
	}

	public T Max()
	{
		return _baseEnumerable.Max();
	}

	public T Min()
	{
		return _baseEnumerable.Min();
	}

	public T Single()
	{
		throw new NotImplementedException();
	}

	public T SingleOrDefault()
	{
		throw new NotImplementedException();
	}

	public T[] ToArray()
	{
		if (_orderedEnumerable is null)
		{
			_orderedEnumerable = GenerateEnumerable();

			_buffer = new Buffer<T, TBaseQuery, TBaseEnumerator>(_baseEnumerable);
			_map = _orderedEnumerable.GetEnumerableSorter(null).Sort(_buffer._items, _buffer._count);
		}

		var array = new T[_buffer._count];

		for (var i = 0; i < array.Length; i++)
		{
			array[i] = _buffer._items[_map[i]];
		}

		return array;
	}

	public List<T> ToList()
	{
		if (_orderedEnumerable is null)
		{
			_orderedEnumerable =GenerateEnumerable();

			_buffer = new Buffer<T, TBaseQuery, TBaseEnumerator>(_baseEnumerable);
			_map = _orderedEnumerable.GetEnumerableSorter(null).Sort(_buffer._items, _buffer._count);
		}

		var list = new List<T>(_buffer._count);
		var span = CollectionsMarshal.AsSpan(list);

		for (var i = 0; i < span.Length; i++)
		{
			span[i] = _buffer._items[_map[i]];
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		return _baseEnumerable.TryGetNonEnumeratedCount(out length);
	}

	public WhereQuery<T, TOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator>, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>> Where<TOperator>() where TOperator : IFunction<T, bool>
	{
		return new WhereQuery<T, TOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator>, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>>(this);
	}

	public SelectQuery<T, TResult, TOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator>, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>> Select<TOperator, TResult>() where TOperator : IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator>, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>>(this);
	}

	public SelectQuery<T, T, TOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator>, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>> Select<TOperator>() where TOperator : IFunction<T, T>
	{
		return new SelectQuery<T, T, TOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator>, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>>(this);
	}

	public SkipQuery<TCount, T, OrderQuery<T, TBaseQuery, TBaseEnumerator>, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, OrderQuery<T, TBaseQuery, TBaseEnumerator>, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>>(this, count);
	}

	public TakeQuery<TCount, T, OrderQuery<T, TBaseQuery, TBaseEnumerator>, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, OrderQuery<T, TBaseQuery, TBaseEnumerator>, OrderEnumerator<T, TBaseQuery, TBaseEnumerator>>(this, count);
	}

	public OrderEnumerator<T, TBaseQuery, TBaseEnumerator> GetEnumerator()
	{
		if (_orderedEnumerable is null)
		{
			_orderedEnumerable = GenerateEnumerable();

			_buffer = new Buffer<T, TBaseQuery, TBaseEnumerator>(_baseEnumerable);
			_map = _orderedEnumerable.GetEnumerableSorter(null).Sort(_buffer._items, _buffer._count);
		}
		
		return new OrderEnumerator<T, TBaseQuery, TBaseEnumerator>(_buffer, _map!);
	}

	private OrderedEnumerable<T, TBaseQuery, TBaseEnumerator> GenerateEnumerable()
	{
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			return new OrderedEnumerable<T, T, TBaseQuery, TBaseEnumerator>(_baseEnumerable, EnumerableSorter<T>.IdentityFunc, _comparer, false, null);
		}

		return new OrderedImplicitlyStableEnumerable<T, TBaseQuery, TBaseEnumerator>(_baseEnumerable, false);
	}
}