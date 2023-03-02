namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public PrependQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Prepend(in TResult item)
	{
		return new PrependQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(ref this, in item);
	}
}