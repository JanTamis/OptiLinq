namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public DefaultIfEmptyQuery<T, RepeatQuery<T>, RepeatEnumerator<T>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, RepeatQuery<T>, RepeatEnumerator<T>>(ref this, defaultValue);
	}
}