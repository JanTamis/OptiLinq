namespace OptiLinq;

public partial struct SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>
{
	public PrependQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>> Prepend(in T item)
	{
		return new PrependQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(ref this, in item);
	}
}