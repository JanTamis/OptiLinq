using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq.Benchmark;

internal struct SelectManyFunction : IFunction<int[], ArrayQuery<int>>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ArrayQuery<int> Eval(in int[] element)
	{
		return element.AsOptiQuery();
	}
}