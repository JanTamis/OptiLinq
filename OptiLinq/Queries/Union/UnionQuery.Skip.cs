using System.Numerics;

namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>
{
	public SkipQuery<TCount, T, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>>(ref this, count);
	}
}