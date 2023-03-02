using System.Numerics;
using OptiLinq.Interfaces;

namespace OptiLinq.Operators;

internal readonly struct AverageOperator<TAccumulator, TNumber> : IAggregateFunction<(TAccumulator, TAccumulator), TNumber, (TAccumulator, TAccumulator)>
	where TAccumulator : INumberBase<TAccumulator>
	where TNumber : INumberBase<TNumber>
{
	public (TAccumulator, TAccumulator) Eval(in (TAccumulator, TAccumulator) accumulator, in TNumber element)
	{
		return (accumulator.Item1 + TAccumulator.CreateChecked(element), accumulator.Item2 + TAccumulator.One);
	}
}