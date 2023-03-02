using System.Numerics;

namespace OptiLinq;

public partial struct TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public TakeQuery<TTakeCount, T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>> Take<TTakeCount>(TTakeCount count)
		where TTakeCount : IBinaryInteger<TTakeCount>
	{
		return new TakeQuery<TTakeCount, T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>(ref this, count);
	}
}