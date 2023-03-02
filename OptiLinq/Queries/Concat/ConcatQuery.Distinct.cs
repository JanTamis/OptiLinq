using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public DistinctQuery<T, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>, TComparer>(ref this, comparer);
	}
}