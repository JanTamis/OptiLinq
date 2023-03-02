using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public IntersectQuery<TResult, TComparer, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<TResult>
		where TComparer : IEqualityComparer<TResult>
	{
		return new IntersectQuery<TResult, TComparer, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<TResult, EqualityComparer<TResult>, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new IntersectQuery<TResult, EqualityComparer<TResult>, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery>(this, other, EqualityComparer<TResult>.Default);
	}
}