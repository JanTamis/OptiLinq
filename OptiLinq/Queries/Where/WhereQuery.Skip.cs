using System.Numerics;

namespace OptiLinq;

public partial struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public SkipQuery<TCount, T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>>(ref this, count);
	}
}