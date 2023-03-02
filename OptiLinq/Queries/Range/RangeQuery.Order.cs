namespace OptiLinq;

public partial struct RangeQuery
{
	public OrderQuery<int, RangeQuery, RangeEnumerator, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<int>
	{
		return new OrderQuery<int, RangeQuery, RangeEnumerator, TComparer>(ref this, comparer);
	}

	public OrderQuery<int, RangeQuery, RangeEnumerator, Comparer<int>> Order()
	{
		return new OrderQuery<int, RangeQuery, RangeEnumerator, Comparer<int>>(ref this, Comparer<int>.Default);
	}
}