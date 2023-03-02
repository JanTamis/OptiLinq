using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public TakeWhileQuery<T, TOperator, SingletonQuery<T>, SingletonEnumerator<T>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, SingletonQuery<T>, SingletonEnumerator<T>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, SingletonQuery<T>, SingletonEnumerator<T>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, SingletonQuery<T>, SingletonEnumerator<T>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}