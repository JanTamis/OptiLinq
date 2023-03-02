using System.Numerics;
using OptiLinq.Interfaces;

public readonly struct IsOdd<TNumber> : IFunction<TNumber, bool> where TNumber : INumberBase<TNumber>
{
	public bool Eval(in TNumber item)
	{
		return TNumber.IsOddInteger(item);
	}
}