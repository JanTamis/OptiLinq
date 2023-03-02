using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RangeQuery
{
	public ConcatQuery<int, RangeQuery, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<int>
	{
		return new ConcatQuery<int, RangeQuery, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}