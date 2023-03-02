using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public TakeWhileQuery<T, TOperator, EnumerableQuery<T>, EnumerableEnumerator<T>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, EnumerableQuery<T>, EnumerableEnumerator<T>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, EnumerableQuery<T>, EnumerableEnumerator<T>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, EnumerableQuery<T>, EnumerableEnumerator<T>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}