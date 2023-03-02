using System.Runtime.CompilerServices;

namespace OptiLinq.Benchmark;

public struct IntComparer : IComparer<int>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int Compare(int x, int y)
	{
		return x - y;
	}
}