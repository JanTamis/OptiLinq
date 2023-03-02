using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public ZipQuery<T, TResult, TOtherOperator, GenerateQuery<T, TOperator>, TOtherQuery> Zip<TResult, TOtherOperator, TOtherQuery>(in TOtherQuery other, TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<T, T, TResult>
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ZipQuery<T, TResult, TOtherOperator, GenerateQuery<T, TOperator>, TOtherQuery>(@operator, in this, in other);
	}

	public ZipQuery<T, TResult, FuncAsIFunction<T, T, TResult>, GenerateQuery<T, TOperator>, TOtherQuery> Zip<TResult, TOtherQuery>(in TOtherQuery other, Func<T, T, TResult> @operator)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ZipQuery<T, TResult, FuncAsIFunction<T, T, TResult>, GenerateQuery<T, TOperator>, TOtherQuery>(new FuncAsIFunction<T, T, TResult>(@operator), in this, in other);
	}
}