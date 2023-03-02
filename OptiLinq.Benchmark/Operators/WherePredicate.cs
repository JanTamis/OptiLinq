using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

public struct WherePredicate : IFunction<int, bool>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly bool Eval(in int element)
	{
		return (element & 1) == 0;
	}
}