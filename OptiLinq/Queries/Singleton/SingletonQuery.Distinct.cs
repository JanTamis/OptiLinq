namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public SingletonQuery<T> Distinct()
	{
		return this;
	}

	public SingletonQuery<T> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return this;
	}
}