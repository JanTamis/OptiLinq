namespace OptiLinq.Collections;

public readonly struct ReverseComparer<T, TBaseComparer> : IComparer<T> where TBaseComparer : IComparer<T>
{
	private readonly TBaseComparer _baseComparer;

	public ReverseComparer(TBaseComparer baseComparer)
	{
		_baseComparer = baseComparer;
	}

	public int Compare(T? x, T? y)
	{
		return -_baseComparer.Compare(x, y);
	}
}