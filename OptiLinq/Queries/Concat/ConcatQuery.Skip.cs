using System.Numerics;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TSecondQuery>
{
	public SkipQuery<TCount, T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>>(ref this, count);
	}
}