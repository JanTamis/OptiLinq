using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>
{
	public ZipQuery<TResult, TOtherResult, TOtherOperator, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery> Zip<TOtherResult, TOtherOperator, TOtherQuery>(in TOtherQuery other, TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<TResult, TResult, TOtherResult>
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ZipQuery<TResult, TOtherResult, TOtherOperator, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery>(@operator, in this, in other);
	}

	public ZipQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TResult, TOtherResult>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery> Zip<TOtherResult, TOtherQuery>(in TOtherQuery other, Func<TResult, TResult, TOtherResult> @operator)
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ZipQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TResult, TOtherResult>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, TOtherQuery>(new FuncAsIFunction<TResult, TResult, TOtherResult>(@operator), in this, in other);
	}
}