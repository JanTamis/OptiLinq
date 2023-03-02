namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public AppendQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Append(in TResult element)
	{
		return new AppendQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(ref this, in element);
	}
}