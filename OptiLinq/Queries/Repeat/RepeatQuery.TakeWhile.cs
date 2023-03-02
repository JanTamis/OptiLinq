using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public TakeWhileQuery<T, TOperator, RepeatQuery<T>, RepeatEnumerator<T>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, RepeatQuery<T>, RepeatEnumerator<T>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, RepeatQuery<T>, RepeatEnumerator<T>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, RepeatQuery<T>, RepeatEnumerator<T>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}