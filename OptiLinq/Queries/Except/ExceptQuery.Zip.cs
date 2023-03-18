using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public ZipQuery<T, TResult, TOperator, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>, TOtherQuery> Zip<TResult, TOperator, TOtherQuery>(in TOtherQuery other, TOperator @operator = default)
		where TOperator : struct, IFunction<T, T, TResult>
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ZipQuery<T, TResult, TOperator, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>, TOtherQuery>(@operator, in this, in other);
	}

	public ZipQuery<T, TResult, FuncAsIFunction<T, T, TResult>, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>, TOtherQuery> Zip<TResult, TOtherQuery>(in TOtherQuery other, Func<T, T, TResult> @operator)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ZipQuery<T, TResult, FuncAsIFunction<T, T, TResult>, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>, TOtherQuery>(new FuncAsIFunction<T, T, TResult>(@operator), in this, in other);
	}
}