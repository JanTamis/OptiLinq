using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>
{
	public TakeWhileQuery<T, TOperator, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}