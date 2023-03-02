using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public ConcatQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}