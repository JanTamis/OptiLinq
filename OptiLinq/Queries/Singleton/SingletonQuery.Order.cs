namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public SingletonQuery<T> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return this;
	}

	public SingletonQuery<T> Order()
	{
		return this;
	}
}