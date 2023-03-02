using System.Numerics;

namespace OptiLinq;

public partial struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public TakeQuery<TCount, T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>>(ref this, count);
	}
}