using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RandomQuery
{
	public ConcatQuery<int, RandomQuery, RandomEnumerator, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<int>
	{
		return new ConcatQuery<int, RandomQuery, RandomEnumerator, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}