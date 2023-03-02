using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>
{
	public ConcatQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ConcatQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}