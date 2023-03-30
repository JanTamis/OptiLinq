// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct RepeatQuery<T>
// {
// 	public OrderQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, TComparer>(ref this, new EnumerableSorter<T, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

