namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public ShuffleQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(ref this, seed);
	}
}