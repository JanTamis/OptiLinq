using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>
{
	public ExceptQuery<TResult, EqualityComparer<TResult>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ExceptQuery<TResult, EqualityComparer<TResult>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery>(this, other, EqualityComparer<TResult>.Default);
	}

	public ExceptQuery<TResult, TOtherComparer, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery> Except<TOtherQuery, TOtherComparer>(in TOtherQuery other, TOtherComparer comparer)
		where TOtherQuery : struct, IOptiQuery<TResult>
		where TOtherComparer : IEqualityComparer<TResult>
	{
		return new ExceptQuery<TResult, TOtherComparer, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery>(this, other, comparer);
	}
}