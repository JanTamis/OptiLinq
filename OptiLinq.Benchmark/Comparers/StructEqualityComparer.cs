using System.Runtime.CompilerServices;

namespace OptiLinq.Benchmark;

internal readonly struct StructEqualityComparer : IEqualityComparer<StructContainer>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(StructContainer x, StructContainer y)
	{
		return x.Element == y.Element;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int GetHashCode(StructContainer obj)
	{
		return obj.Element.GetHashCode();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(in StructContainer x, in StructContainer y)
	{
		return x.Element == y.Element;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int GetHashCode(in StructContainer obj)
	{
		return obj.Element.GetHashCode();
	}
}