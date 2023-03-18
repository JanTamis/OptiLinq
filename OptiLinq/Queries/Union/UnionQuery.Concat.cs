using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>
{
	public ConcatQuery<T, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}