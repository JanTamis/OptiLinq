using System.Runtime.CompilerServices;

namespace OptiLinq.Interfaces;

public struct IdentityComparer<T> : IOptiComparer<T>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int Compare(in T x, in T y)
	{
		return Comparer<T>.Default.Compare(x, y);
	}
}

public readonly struct IdentityComparer<T, TComparer> : IOptiComparer<T>
	where TComparer : IComparer<T>
{
	private readonly TComparer _comparer;

	public IdentityComparer(TComparer comparer)
	{
		_comparer = comparer;
	}

	public int Compare(in T x, in T y)
	{
		return _comparer.Compare(x, y);
	}
}