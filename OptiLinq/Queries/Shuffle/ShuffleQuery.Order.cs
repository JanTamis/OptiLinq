// using OptiLinq.Collections;
// using OptiLinq.Interfaces;
// using OptiLinq.Operators;
//
// namespace OptiLinq;
//
// public partial struct ShuffleQuery<T, TBaseQuery, TBaseEnumerator>
// {
// 	public OrderQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, TComparer>(ref this, new EnumerableSorter<T, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
//
// 	public OrderQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, Comparer<T>> OrderBy<TKey, TKeySelector>(TKeySelector keySelector = default) where TKeySelector : IFunction<T, TKey>
// 	{
// 		return new OrderQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, Comparer<T>>(ref this, new EnumerableSorter<T, TKey, TKeySelector, Comparer<TKey>>(keySelector, Comparer<TKey>.Default, false, null));
// 	}
//
// 	public OrderQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, TComparer> OrderDescending<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, TComparer>(ref this, new EnumerableSorter<T, TComparer>(comparer, true, null));
// 	}
//
// 	public OrderQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, Comparer<T>> OrderDescending()
// 	{
// 		return new OrderQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, true, null));
// 	}
// }

