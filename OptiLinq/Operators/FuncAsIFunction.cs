using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq.Operators;

public readonly struct FuncAsIFunction<TIn1, TIn2, TOut> : IAggregateFunction<TIn1, TIn2, TOut>, IFunction<TIn1, TIn2, TOut>
{
	private readonly Func<TIn1, TIn2, TOut> _func;

	internal FuncAsIFunction(Func<TIn1, TIn2, TOut> func)
	{
		_func = func;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TOut Eval(in TIn1 element1, in TIn2 element2)
	{
		return _func(element1, element2);
	}
}

public readonly struct FuncAsIFunction<TIn, TOut> : IFunction<TIn, TOut>
{
	private readonly Func<TIn, TOut> _func;

	internal FuncAsIFunction(Func<TIn, TOut> func)
	{
		_func = func;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TOut Eval(in TIn element)
	{
		return _func(element);
	}
}

public readonly struct FuncAsIFunction<TOut> : IFunction<TOut>
{
	private readonly Func<TOut> _func;

	internal FuncAsIFunction(Func<TOut> func)
	{
		_func = func;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TOut Eval()
	{
		return _func();
	}
}

public readonly struct ActionAsIFunction<TIn> : IAction<TIn>
{
	private readonly Action<TIn> _action;

	internal ActionAsIFunction(Action<TIn> action)
	{
		_action = action;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Do(in TIn element)
	{
		_action(element);
	}
}