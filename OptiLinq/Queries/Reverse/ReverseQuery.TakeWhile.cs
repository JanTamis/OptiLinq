using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ReverseQuery<T, TBaseQuery, TBaseEnumerator>
{
	public TakeWhileQuery<T, TOperator, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}