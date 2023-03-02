using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public TakeWhileQuery<T, TOperator, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}