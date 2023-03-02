using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RangeQuery
{
	public ConcatQuery<int, RangeQuery, RangeEnumerator, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<int>
	{
		return new ConcatQuery<int, RangeQuery, RangeEnumerator, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}