using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public TakeWhileQuery<T, TOperator, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>> TakeWhile<TOperator>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}