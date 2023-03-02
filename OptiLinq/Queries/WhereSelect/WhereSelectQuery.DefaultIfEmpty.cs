namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public DefaultIfEmptyQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> DefaultIfEmpty(in TResult defaultValue = default)
	{
		return new DefaultIfEmptyQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(ref this, defaultValue);
	}
}