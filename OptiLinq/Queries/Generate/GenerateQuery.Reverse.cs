namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public ReverseQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Reverse()
	{
		return new ReverseQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this);
	}
}