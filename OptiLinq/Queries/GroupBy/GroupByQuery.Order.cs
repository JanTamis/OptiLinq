// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
// {
// 	public OrderQuery<ArrayQuery<T>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<ArrayQuery<T>>
// 	{
// 		return new OrderQuery<ArrayQuery<T>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherComparer>(ref this, new EnumerableSorter<ArrayQuery<T>, TOtherComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<ArrayQuery<T>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, Comparer<ArrayQuery<T>>> Order()
// 	{
// 		return new OrderQuery<ArrayQuery<T>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, Comparer<ArrayQuery<T>>>(ref this, new EnumerableSorter<ArrayQuery<T>, Comparer<ArrayQuery<T>>>(Comparer<ArrayQuery<T>>.Default, false, null));
// 	}
// }

