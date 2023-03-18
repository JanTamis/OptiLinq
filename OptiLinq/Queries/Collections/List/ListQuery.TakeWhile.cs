using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public TakeWhileQuery<T, TOperator, ListQuery<T>, List<T>.Enumerator> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, ListQuery<T>, List<T>.Enumerator>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, ListQuery<T>, List<T>.Enumerator> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, ListQuery<T>, List<T>.Enumerator>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}