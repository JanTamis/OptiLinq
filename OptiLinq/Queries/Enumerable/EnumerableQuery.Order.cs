namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public OrderQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>, Comparer<T>> Order()
	{
		return new OrderQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}