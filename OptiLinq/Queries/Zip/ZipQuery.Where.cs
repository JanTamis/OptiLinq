using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>
{
	public WhereQuery<TResult, TWhereOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<TResult, bool>
	{
		return new WhereQuery<TResult, TWhereOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>>(ref this, @operator);
	}

	public WhereQuery<TResult, FuncAsIFunction<TResult, bool>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>> Where(Func<TResult, bool> @operator)
	{
		return new WhereQuery<TResult, FuncAsIFunction<TResult, bool>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>>(ref this, new FuncAsIFunction<TResult, bool>(@operator));
	}
}