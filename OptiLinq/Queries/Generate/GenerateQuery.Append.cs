namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public AppendQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Append(in T element)
	{
		return new AppendQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, in element);
	}
}