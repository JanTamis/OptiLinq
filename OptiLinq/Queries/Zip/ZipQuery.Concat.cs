using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public ConcatQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ConcatQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}