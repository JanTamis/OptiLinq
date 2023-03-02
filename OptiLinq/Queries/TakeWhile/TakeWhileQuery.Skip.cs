using System.Numerics;

namespace OptiLinq;

public partial struct TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public SkipQuery<TSkipCount, T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>> Skip<TSkipCount>(TSkipCount count)
		where TSkipCount : IBinaryInteger<TSkipCount>
	{
		return new SkipQuery<TSkipCount, T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>(ref this, count);
	}
}