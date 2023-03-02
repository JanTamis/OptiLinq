using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public ConcatQuery<T, HashSetQuery<T>, HashSetEnumerator<T>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, HashSetQuery<T>, HashSetEnumerator<T>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}