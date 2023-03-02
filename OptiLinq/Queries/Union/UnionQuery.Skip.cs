using System.Numerics;

namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>
{
	public SkipQuery<TCount, T, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>>(ref this, count);
	}
}