using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>
{
	public UnionQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, TOtherQuery, EqualityComparer<TResult>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new UnionQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, TOtherQuery, EqualityComparer<TResult>>(ref this, ref other, EqualityComparer<TResult>.Default);
	}

	public UnionQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, TOtherQuery, TOtherComparer> Union<TOtherQuery, TOtherComparer>(TOtherQuery other, TOtherComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<TResult>
		where TOtherComparer : IEqualityComparer<TResult>
	{
		return new UnionQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, TOtherQuery, TOtherComparer>(ref this, ref other, comparer);
	}
}