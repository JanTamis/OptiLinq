using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public TakeWhileQuery<TResult, TOtherOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>> TakeWhile<TOtherOperator>(TOtherOperator @operator)
		where TOtherOperator : struct, IFunction<TResult, bool>
	{
		return new TakeWhileQuery<TResult, TOtherOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>>(this, @operator);
	}

	public TakeWhileQuery<TResult, FuncAsIFunction<TResult, bool>, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>> TakeWhile(Func<TResult, bool> @operator)
	{
		return new TakeWhileQuery<TResult, FuncAsIFunction<TResult, bool>, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>>(this, new FuncAsIFunction<TResult, bool>(@operator));
	}
}