namespace OptiLinq;

public partial struct MemoizeQuery<T, TBaseQuery, TBaseEnumerator>
{
	public OrderQuery<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>, Comparer<T>> Order()
	{
		return new OrderQuery<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}