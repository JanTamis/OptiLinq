using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct IListQuery<T>
{
	public ConcatQuery<T, IListQuery<T>, IListEnumerator<T>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, IListQuery<T>, IListEnumerator<T>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}