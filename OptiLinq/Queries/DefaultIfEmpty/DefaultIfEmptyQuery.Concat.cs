using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>
{
	public ConcatQuery<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}