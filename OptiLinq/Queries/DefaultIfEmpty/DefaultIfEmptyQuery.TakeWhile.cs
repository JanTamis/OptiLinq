using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>
{
	public TakeWhileQuery<T, TOperator, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>> TakeWhile<TOperator>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}