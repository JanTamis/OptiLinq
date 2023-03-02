namespace OptiLinq;

public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>
{
	public OrderQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<TResult>
	{
		return new OrderQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, TOtherComparer>(ref this, comparer);
	}

	public OrderQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, Comparer<TResult>> Order()
	{
		return new OrderQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, Comparer<TResult>>(ref this, Comparer<TResult>.Default);
	}
}