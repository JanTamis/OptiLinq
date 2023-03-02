namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public MemoizeQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Memoize()
	{
		return new MemoizeQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this);
	}
}