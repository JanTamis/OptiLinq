namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public MemoizeQuery<T, RepeatQuery<T>, RepeatEnumerator<T>> Memoize()
	{
		return new MemoizeQuery<T, RepeatQuery<T>, RepeatEnumerator<T>>(ref this);
	}
}