using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>
{
	public ExceptQuery<TResult, EqualityComparer<TResult>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ExceptQuery<TResult, EqualityComparer<TResult>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, TOtherQuery>(this, other, EqualityComparer<TResult>.Default);
	}

	public ExceptQuery<TResult, TOtherComparer, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, TOtherQuery> Except<TOtherQuery, TOtherComparer>(in TOtherQuery other, TOtherComparer comparer)
		where TOtherQuery : struct, IOptiQuery<TResult>
		where TOtherComparer : IEqualityComparer<TResult>
	{
		return new ExceptQuery<TResult, TOtherComparer, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, TOtherQuery>(this, other, comparer);
	}
}