namespace OptiLinq;

public partial struct TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public PrependQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>> Prepend(in T item)
	{
		return new PrependQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(ref this, in item);
	}
}