using System.Numerics;

namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public TakeQuery<TCount, T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>>(ref this, count);
	}
}