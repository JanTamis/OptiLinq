namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public ChunkQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Chunk(int chunkSize)
	{
		return new ChunkQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(ref this, chunkSize);
	}
}