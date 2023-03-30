// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>
// {
// 	public OrderQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, TComparer>(ref this, new EnumerableSorter<T, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

