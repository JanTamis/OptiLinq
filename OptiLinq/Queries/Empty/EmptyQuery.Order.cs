namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return this;
	}

	public EmptyQuery<T> Order()
	{
		return this;
	}
}