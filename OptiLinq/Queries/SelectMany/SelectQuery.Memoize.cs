namespace OptiLinq;

public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>
{
	public MemoizeQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>> Memoize()
	{
		return new MemoizeQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>>(ref this);
	}
}