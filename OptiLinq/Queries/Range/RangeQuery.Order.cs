// using OptiLinq.Collections;
//
// namespace OptiLinq;
//
// public partial struct RangeQuery
// {
// 	public OrderQuery<int, RangeQuery, RangeEnumerator, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<int>
// 	{
// 		return new OrderQuery<int, RangeQuery, RangeEnumerator, TComparer>(ref this, new EnumerableSorter<int, TComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<int, RangeQuery, RangeEnumerator, Comparer<int>> Order()
// 	{
// 		return new OrderQuery<int, RangeQuery, RangeEnumerator, Comparer<int>>(ref this, new EnumerableSorter<int, Comparer<int>>(Comparer<int>.Default, false, null));
// 	}
// }

