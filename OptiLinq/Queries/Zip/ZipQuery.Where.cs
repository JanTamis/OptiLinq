using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public WhereQuery<TResult, TWhereOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<TResult, bool>
	{
		return new WhereQuery<TResult, TWhereOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>>(ref this, @operator);
	}

	public WhereQuery<TResult, FuncAsIFunction<TResult, bool>, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>> Where(Func<TResult, bool> @operator)
	{
		return new WhereQuery<TResult, FuncAsIFunction<TResult, bool>, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>>(ref this, new FuncAsIFunction<TResult, bool>(@operator));
	}
}