using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public ConcatQuery<T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}