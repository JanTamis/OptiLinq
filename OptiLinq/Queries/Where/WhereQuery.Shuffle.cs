namespace OptiLinq;

public partial struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public ShuffleQuery<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>>(ref this, seed);
	}
}