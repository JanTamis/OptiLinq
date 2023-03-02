using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>
{
	public TakeWhileQuery<T, TOperator, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}