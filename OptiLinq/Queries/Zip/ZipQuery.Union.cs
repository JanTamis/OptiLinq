using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public UnionQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery, EqualityComparer<TResult>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new UnionQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery, EqualityComparer<TResult>>(ref this, ref other, EqualityComparer<TResult>.Default);
	}

	public UnionQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery, TOtherComparer> Union<TOtherQuery, TOtherComparer>(TOtherQuery other, TOtherComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<TResult>
		where TOtherComparer : IEqualityComparer<TResult>
	{
		return new UnionQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery, TOtherComparer>(ref this, ref other, comparer);
	}
}