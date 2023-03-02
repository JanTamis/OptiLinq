using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>
{
	public UnionQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery, EqualityComparer<TResult>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new UnionQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery, EqualityComparer<TResult>>(ref this, ref other, EqualityComparer<TResult>.Default);
	}

	public UnionQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery, TOtherComparer> Union<TOtherQuery, TOtherComparer>(TOtherQuery other, TOtherComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<TResult>
		where TOtherComparer : IEqualityComparer<TResult>
	{
		return new UnionQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery, TOtherComparer>(ref this, ref other, comparer);
	}
}