namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, Comparer<T>> Order()
	{
		return new OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}