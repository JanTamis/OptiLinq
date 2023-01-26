using System.Numerics;
using System.Runtime.InteropServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public readonly struct RepeatQuery<T> : IOptiQuery<T, RepeatEnumerator<T>>
{
	private readonly  int _count;
	private readonly T _element;

	internal RepeatQuery(T element, int count)
	{
		_element = element;
		_count = count;
	}

	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		return TAllOperator.Eval(_element);
	}

	public bool Any()
	{
		return _count > 0;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, RepeatQuery<T>, RepeatEnumerator<T>>(this);
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer = null)
	{
		comparer ??= EqualityComparer<T>.Default;
		
		return comparer.Equals(item, _element);
	}

	public int Count()
	{
		return _count;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero && Int32.CreateChecked(index) < _count)
		{
			return _element;
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero && Int32.CreateChecked(index) < _count)
		{
			return _element;
		}

		return default;
	}

	public T First()
	{
		if (_count <= 0)
		{
			throw new Exception("Sequence doesn't contain a element");
		}

		return _element;
	}

	public T FirstOrDefault()
	{
		return _element;
	}

	public T Last()
	{
		if (_count <= 0)
		{
			throw new Exception("Sequence doesn't contain a element");
		}

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

	public T Single()
	{
		if (_count == 1)
		{
			return _element;
		}

		throw new Exception("Sequence contains contains to much elements");
	}

	public T SingleOrDefault()
	{
		if (_count == 1)
		{
			return _element;
		}

		return default;
	}

	public T[] ToArray()
	{
		var array = new T[_count];
		
		Array.Fill(array, _element);

		return array;
	}

	public T[] ToArray(out int length)
	{
		length = _count;
		return ToArray();
	}

	public List<T> ToList()
	{
		var list = new List<T>(_count);
		
		CollectionsMarshal
			.AsSpan(list)
			.Fill(_element);

		return list;
	}

	public WhereQuery<T, TOperator, RepeatQuery<T>, RepeatEnumerator<T>> Where<TOperator>() where TOperator : IFunction<T, bool>
	{
		return new WhereQuery<T, TOperator, RepeatQuery<T>, RepeatEnumerator<T>>(this);
	}

	public SelectQuery<T, TResult, TOperator, RepeatQuery<T>, RepeatEnumerator<T>> Select<TOperator, TResult>() where TOperator : IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TOperator, RepeatQuery<T>, RepeatEnumerator<T>>(this);
	}

	public SelectQuery<T, T, TOperator, RepeatQuery<T>, RepeatEnumerator<T>> Select<TOperator>() where TOperator : IFunction<T, T>
	{
		return new SelectQuery<T, T, TOperator, RepeatQuery<T>, RepeatEnumerator<T>>(this);
	}

	public SkipQuery<TCount, T, RepeatQuery<T>, RepeatEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, RepeatQuery<T>, RepeatEnumerator<T>>(this, count);
	}

	public TakeQuery<TCount, T, RepeatQuery<T>, RepeatEnumerator<T>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, RepeatQuery<T>, RepeatEnumerator<T>>(this, count);
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_count < 0)
		{
			length = 0;
			return false;
		}
		
		length = _count;
		return true;
	}

	public RepeatEnumerator<T> GetEnumerator()
	{
		return new RepeatEnumerator<T>(_element, _count);
	}
}