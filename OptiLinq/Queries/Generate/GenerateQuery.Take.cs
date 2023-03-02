using System.Numerics;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public TakeQuery<TCount, T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, count);
	}
}