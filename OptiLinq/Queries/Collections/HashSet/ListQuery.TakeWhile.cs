using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public TakeWhileQuery<T, TOperator, HashSetQuery<T>, HashSetEnumerator<T>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, HashSetQuery<T>, HashSetEnumerator<T>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, HashSetQuery<T>, HashSetEnumerator<T>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, HashSetQuery<T>, HashSetEnumerator<T>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}