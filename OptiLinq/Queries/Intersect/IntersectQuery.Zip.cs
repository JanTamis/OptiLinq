using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public ZipQuery<T, TResult, TOperator, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery> Zip<TResult, TOperator, TOtherQuery>(in TOtherQuery other, TOperator @operator = default)
		where TOperator : struct, IFunction<T, T, TResult>
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ZipQuery<T, TResult, TOperator, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery>(@operator, in this, in other);
	}

	public ZipQuery<T, TResult, FuncAsIFunction<T, T, TResult>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery> Zip<TResult, TOtherQuery>(in TOtherQuery other, Func<T, T, TResult> @operator)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ZipQuery<T, TResult, FuncAsIFunction<T, T, TResult>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery>(new FuncAsIFunction<T, T, TResult>(@operator), in this, in other);
	}
}