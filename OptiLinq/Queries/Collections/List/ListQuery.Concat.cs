using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public ConcatQuery<T, ListQuery<T>, List<T>.Enumerator, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, ListQuery<T>, List<T>.Enumerator, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}