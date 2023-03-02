using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public TakeWhileQuery<T, TOperator, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}