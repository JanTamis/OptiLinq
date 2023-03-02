using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public TakeWhileQuery<T, TOperator, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}