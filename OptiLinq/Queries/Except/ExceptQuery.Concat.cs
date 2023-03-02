using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public ConcatQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}