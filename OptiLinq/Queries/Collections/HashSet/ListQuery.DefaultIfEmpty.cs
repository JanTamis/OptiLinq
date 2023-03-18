namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public DefaultIfEmptyQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, defaultValue);
	}
}