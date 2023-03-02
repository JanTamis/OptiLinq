using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ShuffleQuery<T, TBaseQuery, TBaseEnumerator>
{
	public ConcatQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}