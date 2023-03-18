using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public ConcatQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}