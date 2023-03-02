using System.Numerics;

namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public SkipQuery<TCount, T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>>(ref this, count);
	}
}