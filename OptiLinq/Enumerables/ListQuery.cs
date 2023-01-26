using System.Numerics;
using System.Runtime.InteropServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public readonly struct ListQuery<T> : IOptiQuery<T, ListEnumerator<T>>
{
	private readonly IList<T> _list;

	internal ListQuery(IList<T> list)
	{
		_list = list;
	}

	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		for (var i = 0; i < _list.Count; i++)
		{
			if (!TAllOperator.Eval(_list[i]))
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

	public IEnumerable<T> AsEnumerable()
	{
		return _list;
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer = null)
	{
		comparer ??= EqualityComparer<T>.Default;
		
		for (var i = 0; i < _list.Count; i++)
		{
			if (comparer.Equals(_list[i], item))
			{
				return false;
			}
		}

		return true;
	}

	public int Count()
	{
		return _list.Count;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
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
		T? value = default;

		using var enumerator = GetEnumerator();

		if (value == null)
		{
			do
			{
				if (!enumerator.MoveNext())
				{
					return value;
				}

				value = enumerator.Current;
			} while (value == null);

			while (enumerator.MoveNext())
			{
				var next = enumerator.Current;
				if (next != null && Comparer<T>.Default.Compare(next, value) > 0)
				{
					value = next;
				}
			}
		}
		else
		{
			if (!enumerator.MoveNext())
			{
				throw new Exception("Collection is empty");
			}

			value = enumerator.Current;

			while (enumerator.MoveNext())
			{
				var next = enumerator.Current;

				if (Comparer<T>.Default.Compare(next, value) > 0)
				{
					value = next;
				}
			}
		}

		return value;
	}

	public T Min()
	{
		T? value = default;

		using var enumerator = GetEnumerator();

		if (value == null)
		{
			do
			{
				if (!enumerator.MoveNext())
				{
					return value;
				}

				value = enumerator.Current;
			} while (value == null);

			while (enumerator.MoveNext())
			{
				var next = enumerator.Current;
				if (next != null && Comparer<T>.Default.Compare(next, value) < 0)
				{
					value = next;
				}
			}
		}
		else
		{
			if (!enumerator.MoveNext())
			{
				throw new Exception("Collection is empty");
			}

			value = enumerator.Current;

			while (enumerator.MoveNext())
			{
				var next = enumerator.Current;

				if (Comparer<T>.Default.Compare(next, value) < 0)
				{
					value = next;
				}
			}
		}

		return value;
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

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = _list.Count;
		return true;
	}

	public WhereQuery<T, TOperator, ListQuery<T>, ListEnumerator<T>> Where<TOperator>() where TOperator : IFunction<T, bool>
	{
		return new WhereQuery<T, TOperator, ListQuery<T>, ListEnumerator<T>>(this);
	}

	public SelectQuery<T, TResult, TOperator, ListQuery<T>, ListEnumerator<T>> Select<TOperator, TResult>() where TOperator : IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TOperator, ListQuery<T>, ListEnumerator<T>>(this);
	}

	public SelectQuery<T, T, TOperator, ListQuery<T>, ListEnumerator<T>> Select<TOperator>() where TOperator : IFunction<T, T>
	{
		return new SelectQuery<T, T, TOperator, ListQuery<T>, ListEnumerator<T>>(this);
	}

	public SkipQuery<TCount, T, ListQuery<T>, ListEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, ListQuery<T>, ListEnumerator<T>>(this, count);
	}

	public TakeQuery<TCount, T, ListQuery<T>, ListEnumerator<T>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, ListQuery<T>, ListEnumerator<T>>(this, count);
	}

	public ListEnumerator<T> GetEnumerator()
	{
		return new ListEnumerator<T>(_list);
	}
}