using System.Numerics;
using OptiLinq.Interfaces;

namespace OptiLinq.Operators;

internal readonly struct NumberConvert<TSourceNumber, TResultNumber> : IFunction<TSourceNumber, TResultNumber>
	where TSourceNumber : INumberBase<TSourceNumber>
	where TResultNumber : INumberBase<TResultNumber>
{
	public TResultNumber Eval(in TSourceNumber element)
	{
		return TResultNumber.CreateSaturating(element);
	}
}