using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TSecondQuery>
{
	public TakeWhileQuery<T, TOperator, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>> TakeWhile<TOperator>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}