namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public OrderQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator, Comparer<T>> Order()
	{
		return new OrderQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}