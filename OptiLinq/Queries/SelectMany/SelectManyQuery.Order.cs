// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>
// {
// 	public OrderQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<TResult>
// 	{
// 		return new OrderQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, TOtherComparer>(ref this, new EnumerableSorter<TResult, TOtherComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, Comparer<TResult>> Order()
// 	{
// 		return new OrderQuery<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, Comparer<TResult>>(ref this, new EnumerableSorter<TResult, Comparer<TResult>>(Comparer<TResult>.Default, false, null));
// 	}
// }

