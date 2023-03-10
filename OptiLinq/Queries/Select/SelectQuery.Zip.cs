using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>
{
	public ZipQuery<TResult, TOtherResult, TOtherOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TOtherQuery> Zip<TOtherResult, TOtherOperator, TOtherQuery>(in TOtherQuery other, TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<TResult, TResult, TOtherResult>
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ZipQuery<TResult, TOtherResult, TOtherOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TOtherQuery>(@operator, in this, in other);
	}

	public ZipQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TResult, TOtherResult>, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TOtherQuery> Zip<TOtherResult, TOtherQuery>(in TOtherQuery other, Func<TResult, TResult, TOtherResult> @operator)
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ZipQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TResult, TOtherResult>, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TOtherQuery>(new FuncAsIFunction<TResult, TResult, TOtherResult>(@operator), in this, in other);
	}
}