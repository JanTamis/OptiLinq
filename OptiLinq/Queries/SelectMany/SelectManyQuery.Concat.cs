using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>
{
	public ConcatQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, TOtherQuery> Concat<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ConcatQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, TOtherQuery>(ref this, ref Unsafe.AsRef(in other));
	}
}