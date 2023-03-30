// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct HashSetQuery<T>
// {
// 	public OrderQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator, TComparer>(ref this, new EnumerableSorter<T, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

