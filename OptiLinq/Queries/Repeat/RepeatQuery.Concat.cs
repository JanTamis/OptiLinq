using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public ConcatQuery<T, RepeatQuery<T>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, RepeatQuery<T>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}