namespace OptiLinq;

public partial struct ListQuery<T>
{
	public OrderQuery<T, ListQuery<T>, List<T>.Enumerator, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, ListQuery<T>, List<T>.Enumerator, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, ListQuery<T>, List<T>.Enumerator, Comparer<T>> Order()
	{
		return new OrderQuery<T, ListQuery<T>, List<T>.Enumerator, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}