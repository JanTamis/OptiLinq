using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TSecondQuery>
{
	public ConcatQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}