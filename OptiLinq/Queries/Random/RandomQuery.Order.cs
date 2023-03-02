namespace OptiLinq;

public partial struct RandomQuery
{
	public OrderQuery<int, RandomQuery, RandomEnumerator, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<int>
	{
		return new OrderQuery<int, RandomQuery, RandomEnumerator, TComparer>(ref this, comparer);
	}

	public OrderQuery<int, RandomQuery, RandomEnumerator, Comparer<int>> Order()
	{
		return new OrderQuery<int, RandomQuery, RandomEnumerator, Comparer<int>>(ref this, Comparer<int>.Default);
	}
}