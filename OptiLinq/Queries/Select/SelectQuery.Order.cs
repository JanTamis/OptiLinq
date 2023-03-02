namespace OptiLinq;

public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>
{
	public OrderQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<TResult>
	{
		return new OrderQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TOtherComparer>(ref this, comparer);
	}

	public OrderQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, Comparer<TResult>> Order()
	{
		return new OrderQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, Comparer<TResult>>(ref this, Comparer<TResult>.Default);
	}
}