using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public TakeWhileQuery<T, TOperator, ListQuery<T>, ListEnumerator<T>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, ListQuery<T>, ListEnumerator<T>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, ListQuery<T>, ListEnumerator<T>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, ListQuery<T>, ListEnumerator<T>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}