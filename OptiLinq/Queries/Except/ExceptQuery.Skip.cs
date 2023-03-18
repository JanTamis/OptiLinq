using System.Numerics;

namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public SkipQuery<TCount, T, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>>(ref this, count);
	}
}