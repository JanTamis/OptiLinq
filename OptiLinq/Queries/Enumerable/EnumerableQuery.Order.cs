// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct EnumerableQuery<T>
// {
// 	public OrderQuery<T, EnumerableQuery<T>, IEnumerator<T>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, EnumerableQuery<T>, IEnumerator<T>, TComparer>(ref this, new EnumerableSorter<T, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, EnumerableQuery<T>, IEnumerator<T>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, EnumerableQuery<T>, IEnumerator<T>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

