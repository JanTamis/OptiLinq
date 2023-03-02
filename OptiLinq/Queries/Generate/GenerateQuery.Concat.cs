using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public ConcatQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ConcatQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}