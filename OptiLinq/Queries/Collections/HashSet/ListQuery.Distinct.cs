namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public HashSetQuery<T> Distinct()
	{
		return this;
	}

	public HashSetQuery<T> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return this;
	}
}