using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq.Operators;

public readonly struct SumOperator<TAccumulator> : IAggregateFunction<TAccumulator, TAccumulator, TAccumulator>
	where TAccumulator : INumberBase<TAccumulator>
{
	public TAccumulator Eval(in TAccumulator accumulator, in TAccumulator element)
	{
		return accumulator + element;
	}
}

public readonly struct SumOperator<T, TNumber> : IAggregateFunction<TNumber, T, TNumber>
	where TNumber : INumberBase<TNumber>
{
	private readonly Func<T, TNumber> _selector;

	public SumOperator(Func<T, TNumber> selector)
	{
		_selector = selector;
	}

	public TNumber Eval(in TNumber accumulator, in T element)
	{
		return accumulator + _selector(element);
	}
}

public struct SumOperator<T, TNumber, TSelector> : IAggregateFunction<TNumber, T, TNumber>
	where TNumber : INumberBase<TNumber>
	where TSelector : struct, IFunction<T, TNumber>
{
	private TSelector _selector;

	public SumOperator(TSelector selector)
	{
		_selector = selector;
	}

	public TNumber Eval(in TNumber accumulator, in T element)
	{
		return accumulator + _selector.Eval(in element);
	}
}