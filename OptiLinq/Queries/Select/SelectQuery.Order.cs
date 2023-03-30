// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>
// {
// 	public OrderQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<TResult>
// 	{
// 		return new OrderQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TOtherComparer>(ref this, new EnumerableSorter<TResult, TOtherComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, Comparer<TResult>> Order()
// 	{
// 		return new OrderQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, Comparer<TResult>>(ref this, new EnumerableSorter<TResult, Comparer<TResult>>(Comparer<TResult>.Default, false, null));
// 	}
// }

