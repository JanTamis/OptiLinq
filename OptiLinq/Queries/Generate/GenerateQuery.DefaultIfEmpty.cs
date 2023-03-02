namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public DefaultIfEmptyQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, defaultValue);
	}
}