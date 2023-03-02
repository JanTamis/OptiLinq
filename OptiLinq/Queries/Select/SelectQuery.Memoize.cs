namespace OptiLinq;

public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>
{
	public MemoizeQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>> Memoize()
	{
		return new MemoizeQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(ref this);
	}
}