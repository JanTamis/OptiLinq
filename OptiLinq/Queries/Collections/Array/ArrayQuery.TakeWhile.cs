using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public TakeWhileQuery<T, TOperator, ArrayQuery<T>, ArrayEnumerator<T>> TakeWhile<TOperator>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, ArrayQuery<T>, ArrayEnumerator<T>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, ArrayQuery<T>, ArrayEnumerator<T>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, ArrayQuery<T>, ArrayEnumerator<T>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}