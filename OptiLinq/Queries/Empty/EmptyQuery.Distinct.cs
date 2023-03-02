namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> Distinct()
	{
		return this;
	}

	public EmptyQuery<T> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return this;
	}
}