namespace OptiLinq.Interfaces;

public struct IdentityComparerDescending<T> : IOptiComparer<T>
{
	public int Compare(in T x, in T y)
	{
		return -Comparer<T>.Default.Compare(x, y);
	}
}

public readonly struct IdentityComparerDescending<T, TComparer> : IOptiComparer<T>
	where TComparer : IComparer<T>
{
	private readonly TComparer _comparer;

	public IdentityComparerDescending(TComparer comparer)
	{
		_comparer = comparer;
	}

	public int Compare(in T x, in T y)
	{
		return -_comparer.Compare(x, y);
	}
}