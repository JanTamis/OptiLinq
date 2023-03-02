using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public ZipQuery<T, TResult, TOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery> Zip<TResult, TOperator, TOtherQuery>(in TOtherQuery other, TOperator @operator = default)
		where TOperator : struct, IFunction<T, T, TResult>
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ZipQuery<T, TResult, TOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery>(@operator, in this, in other);
	}

	public ZipQuery<T, TResult, FuncAsIFunction<T, T, TResult>, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery> Zip<TResult, TOtherQuery>(in TOtherQuery other, Func<T, T, TResult> @operator)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ZipQuery<T, TResult, FuncAsIFunction<T, T, TResult>, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery>(new FuncAsIFunction<T, T, TResult>(@operator), in this, in other);
	}
}