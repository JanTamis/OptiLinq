namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public ShuffleQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, seed);
	}
}