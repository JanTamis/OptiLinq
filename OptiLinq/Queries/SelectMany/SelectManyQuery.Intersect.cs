using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>
{
	public IntersectQuery<TResult, TComparer, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<TResult>
		where TComparer : IEqualityComparer<TResult>
	{
		return new IntersectQuery<TResult, TComparer, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<TResult, EqualityComparer<TResult>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new IntersectQuery<TResult, EqualityComparer<TResult>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery>(this, other, EqualityComparer<TResult>.Default);
	}
}