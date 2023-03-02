namespace OptiLinq;

public partial struct ListQuery<T>
{
	public OrderQuery<T, ListQuery<T>, ListEnumerator<T>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, ListQuery<T>, ListEnumerator<T>, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, ListQuery<T>, ListEnumerator<T>, Comparer<T>> Order()
	{
		return new OrderQuery<T, ListQuery<T>, ListEnumerator<T>, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}