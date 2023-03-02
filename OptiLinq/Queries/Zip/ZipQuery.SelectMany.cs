using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>
{
	public SelectManyQuery<TResult, TOtherResult, TSelectOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, IOptiQuery<TOtherResult>> SelectMany<TSelectOperator, TOtherResult>(TSelectOperator @operator = default)
		where TSelectOperator : struct, IFunction<TResult, IOptiQuery<TOtherResult>>
	{
		return new SelectManyQuery<TResult, TOtherResult, TSelectOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, IOptiQuery<TOtherResult>>(ref this, @operator);
	}

	public SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, IOptiQuery<TOtherResult>> SelectMany<TOtherResult>(Func<TResult, IOptiQuery<TOtherResult>> @operator)
	{
		return new SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, IOptiQuery<TOtherResult>>(ref this, new FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>(@operator));
	}

	public SelectManyQuery<TResult, TOtherResult, TOtherOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, TSubQuery> SelectMany<TOtherOperator, TSubQuery, TOtherResult>(TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<TResult, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TOtherResult>
	{
		return new SelectManyQuery<TResult, TOtherResult, TOtherOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TSubQuery>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, TSubQuery> SelectMany<TSubQuery, TOtherResult>(Func<TResult, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TOtherResult>
	{
		return new SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TSubQuery>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, TSubQuery>(ref this, new FuncAsIFunction<TResult, TSubQuery>(@operator));
	}
}