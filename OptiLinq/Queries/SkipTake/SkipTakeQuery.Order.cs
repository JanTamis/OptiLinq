// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>
// {
// 	public OrderQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>, TComparer>(ref this, new EnumerableSorter<T, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

