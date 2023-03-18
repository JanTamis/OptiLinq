using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public TakeWhileQuery<T, TOperator, EnumerableQuery<T>, IEnumerator<T>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, EnumerableQuery<T>, IEnumerator<T>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, EnumerableQuery<T>, IEnumerator<T>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, EnumerableQuery<T>, IEnumerator<T>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}