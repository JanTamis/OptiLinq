// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
// {
// 	public OrderQuery<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, TOtherComparer>(ref this, new EnumerableSorter<T, TOtherComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

