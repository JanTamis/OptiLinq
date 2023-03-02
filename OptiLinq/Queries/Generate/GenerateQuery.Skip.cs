using System.Numerics;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public SkipQuery<TCount, T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, count);
	}
}