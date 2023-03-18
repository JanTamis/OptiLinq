using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public ZipQuery<TResult, TOtherResult, TOtherOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>, TOtherQuery> Zip<TOtherResult, TOtherOperator, TOtherQuery>(in TOtherQuery other, TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<TResult, TResult, TOtherResult>
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ZipQuery<TResult, TOtherResult, TOtherOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>, TOtherQuery>(@operator, in this, in other);
	}

	public ZipQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TResult, TOtherResult>, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>, TOtherQuery> Zip<TOtherResult, TOtherQuery>(in TOtherQuery other, Func<TResult, TResult, TOtherResult> @operator)
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ZipQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TResult, TOtherResult>, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>, TOtherQuery>(new FuncAsIFunction<TResult, TResult, TOtherResult>(@operator), in this, in other);
	}
}