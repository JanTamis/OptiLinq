using System.Numerics;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public TakeLastQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> TakeLast(int count)
	{
		return new TakeLastQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(this, count);
	}
}