using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public IntersectQuery<TResult, TComparer, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<TResult>
		where TComparer : IEqualityComparer<TResult>
	{
		return new IntersectQuery<TResult, TComparer, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<TResult, EqualityComparer<TResult>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new IntersectQuery<TResult, EqualityComparer<TResult>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, TOtherQuery>(this, other, EqualityComparer<TResult>.Default);
	}
}