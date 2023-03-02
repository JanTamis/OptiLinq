namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public AppendQuery<T, RepeatQuery<T>, RepeatEnumerator<T>> Append(in T element)
	{
		return new AppendQuery<T, RepeatQuery<T>, RepeatEnumerator<T>>(ref this, in element);
	}
}