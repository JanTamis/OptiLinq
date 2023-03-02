using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>
{
	public TakeWhileQuery<TResult, TOtherOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>> TakeWhile<TOtherOperator>(TOtherOperator @operator)
		where TOtherOperator : struct, IFunction<TResult, bool>
	{
		return new TakeWhileQuery<TResult, TOtherOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>>(this, @operator);
	}

	public TakeWhileQuery<TResult, FuncAsIFunction<TResult, bool>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>> TakeWhile(Func<TResult, bool> @operator)
	{
		return new TakeWhileQuery<TResult, FuncAsIFunction<TResult, bool>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>>(this, new FuncAsIFunction<TResult, bool>(@operator));
	}
}