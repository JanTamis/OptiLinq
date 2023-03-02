namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public PrependQuery<T, RepeatQuery<T>, RepeatEnumerator<T>> Prepend(in T item)
	{
		return new PrependQuery<T, RepeatQuery<T>, RepeatEnumerator<T>>(ref this, in item);
	}
}