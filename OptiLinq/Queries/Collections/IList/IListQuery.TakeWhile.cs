using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct IListQuery<T>
{
	public TakeWhileQuery<T, TOperator, IListQuery<T>, IListEnumerator<T>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, IListQuery<T>, IListEnumerator<T>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, IListQuery<T>, IListEnumerator<T>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, IListQuery<T>, IListEnumerator<T>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}