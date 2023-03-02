namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public PrependQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Prepend(in T item)
	{
		return new PrependQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, in item);
	}
}