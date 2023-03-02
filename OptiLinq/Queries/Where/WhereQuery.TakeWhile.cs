using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public TakeWhileQuery<T, TOtherOperator, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>> TakeWhile<TOtherOperator>(TOtherOperator @operator)
		where TOtherOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOtherOperator, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}