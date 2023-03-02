using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>
{
	public TakeWhileQuery<TResult, TOtherOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>> TakeWhile<TOtherOperator>(TOtherOperator @operator)
		where TOtherOperator : struct, IFunction<TResult, bool>
	{
		return new TakeWhileQuery<TResult, TOtherOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(this, @operator);
	}

	public TakeWhileQuery<TResult, FuncAsIFunction<TResult, bool>, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>> TakeWhile(Func<TResult, bool> @operator)
	{
		return new TakeWhileQuery<TResult, FuncAsIFunction<TResult, bool>, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(this, new FuncAsIFunction<TResult, bool>(@operator));
	}
}