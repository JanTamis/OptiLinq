using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>
{
	public ConcatQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ConcatQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}