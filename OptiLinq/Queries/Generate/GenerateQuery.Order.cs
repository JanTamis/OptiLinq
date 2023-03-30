// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct GenerateQuery<T, TOperator>
// {
// 	public OrderQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TComparer>(ref this, new EnumerableSorter<T, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, Comparer<T>> Order()
// 	{
// 		return new OrderQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, Comparer<T>>(ref this, new EnumerableSorter<T, Comparer<T>>(Comparer<T>.Default, false, null));
// 	}
// }

