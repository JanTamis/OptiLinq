namespace OptiLinq;

public partial struct SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>
{
	public MemoizeQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>> Memoize()
	{
		return new MemoizeQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(ref this);
	}
}