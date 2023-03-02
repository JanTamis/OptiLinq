using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public ConcatQuery<T, ArrayQuery<T>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, ArrayQuery<T>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}