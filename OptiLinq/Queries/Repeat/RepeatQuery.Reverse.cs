namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public ReverseQuery<T, RepeatQuery<T>, RepeatEnumerator<T>> Reverse()
	{
		return new ReverseQuery<T, RepeatQuery<T>, RepeatEnumerator<T>>(ref this);
	}
}