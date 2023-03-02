using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public ConcatQuery<T, SingletonQuery<T>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, SingletonQuery<T>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}