// using OptiLinq.Collections;
// using OptiLinq.Comparers;
//
// namespace OptiLinq;
//
// public partial struct ChunkQuery<T, TBaseQuery, TBaseEnumerator>
// {
// 	public OrderQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T[]>
// 	{
// 		return new OrderQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, TComparer>(ref this, new EnumerableSorter<T[], TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, ArrayComparer<T>> Order()
// 	{
// 		return new OrderQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, ArrayComparer<T>>(ref this, new EnumerableSorter<T[], ArrayComparer<T>>(new ArrayComparer<T>(), false, null));
// 	}
// }

