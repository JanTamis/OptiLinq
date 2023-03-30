// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
// {
// 	public OrderQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>, TOtherComparer>(ref this, new EnumerableSorter<T, TOtherComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

