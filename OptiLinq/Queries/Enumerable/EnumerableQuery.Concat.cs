using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public ConcatQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}