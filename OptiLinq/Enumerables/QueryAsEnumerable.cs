using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public class QueryAsEnumerable<T, TBaseQuery, TBaseEnumerator> : IEnumerable<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseQuery _baseQuery;

	public QueryAsEnumerable(TBaseQuery baseQuery)
	{
		_baseQuery = baseQuery;
	}

	public IEnumerator<T> GetEnumerator()
	{
		return new QueryEnumerator(_baseQuery.GetEnumerator());
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public class QueryEnumerator : IEnumerator<T>
	{
		private TBaseEnumerator _enumerator;

		public QueryEnumerator(TBaseEnumerator enumerator)
		{
			_enumerator = enumerator;
		}

		public bool MoveNext()
		{
			return _enumerator.MoveNext();
		}

		public void Reset()
		{
			
		}

		public T Current => _enumerator.Current;

		object IEnumerator.Current => Current;

		public void Dispose()
		{
			_enumerator.Dispose();
		}
	}
}