namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public DefaultIfEmptyQuery<T, HashSetQuery<T>, HashSetEnumerator<T>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, HashSetQuery<T>, HashSetEnumerator<T>>(ref this, defaultValue);
	}
}