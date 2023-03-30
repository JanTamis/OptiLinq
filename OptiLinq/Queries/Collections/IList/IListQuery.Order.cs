// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct IListQuery<T>
// {
// 	public OrderQuery<T, IListQuery<T>, IListEnumerator<T>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, IListQuery<T>, IListEnumerator<T>, TComparer>(ref this, new EnumerableSorter<T, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, IListQuery<T>, IListEnumerator<T>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, IListQuery<T>, IListEnumerator<T>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

