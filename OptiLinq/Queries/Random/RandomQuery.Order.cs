// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct RandomQuery
// {
// 	public OrderQuery<int, RandomQuery, RandomEnumerator, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<int>
// 	{
// 		return new OrderQuery<int, RandomQuery, RandomEnumerator, TComparer>(ref this, new EnumerableSorter<int, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<int, RandomQuery, RandomEnumerator, Comparer<int>> Order()
// 	{
// 		return new OrderQuery<int, RandomQuery, RandomEnumerator, Comparer<int>>(ref this, new EnumerableSorter<int, Comparer<int>>(Comparer<int>.Default, false, null));
// 	}
// }

