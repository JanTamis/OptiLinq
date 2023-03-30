using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

public readonly struct IsEven<TNumber> : IFunction<TNumber, bool> where TNumber : INumberBase<TNumber>
{
	public bool Eval(in TNumber item)
	{
		return TNumber.IsEvenInteger(item);
	}
}