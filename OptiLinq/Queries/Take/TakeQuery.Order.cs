namespace OptiLinq;

public partial struct TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public OrderQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, Comparer<T>> Order()
	{
		return new OrderQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}