// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
// {
// 	public OrderQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator, TComparer>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator, TComparer>, TOtherComparer>(ref this, new EnumerableSorter<T, TOtherComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator, TComparer>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator, TComparer>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

