namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public OrderQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, Comparer<T>> Order()
	{
		return new OrderQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}