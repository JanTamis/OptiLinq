// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>
// {
// 	public OrderQuery<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>, TComparer>(ref this, new EnumerableSorter<T, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }
//
