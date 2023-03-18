using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public TakeWhileQuery<T, TOperator, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}