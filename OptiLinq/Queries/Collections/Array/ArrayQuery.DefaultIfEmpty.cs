namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public DefaultIfEmptyQuery<T, ArrayQuery<T>, ArrayEnumerator<T>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, defaultValue);
	}
}