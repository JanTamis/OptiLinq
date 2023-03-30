// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
// {
// 	public OrderQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TOtherComparer>(ref this, new EnumerableSorter<T, TOtherComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

