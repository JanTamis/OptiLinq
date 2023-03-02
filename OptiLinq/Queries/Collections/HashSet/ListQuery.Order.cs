namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public OrderQuery<T, HashSetQuery<T>, HashSetEnumerator<T>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, HashSetQuery<T>, HashSetEnumerator<T>, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, HashSetQuery<T>, HashSetEnumerator<T>, Comparer<T>> Order()
	{
		return new OrderQuery<T, HashSetQuery<T>, HashSetEnumerator<T>, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}