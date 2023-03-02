using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
{
	public TakeWhileQuery<ArrayQuery<T>, TOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<ArrayQuery<T>, bool>
	{
		return new TakeWhileQuery<ArrayQuery<T>, TOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>>(this, @operator);
	}

	public TakeWhileQuery<ArrayQuery<T>, FuncAsIFunction<ArrayQuery<T>, bool>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>> TakeWhile(Func<ArrayQuery<T>, bool> @operator)
	{
		return new TakeWhileQuery<ArrayQuery<T>, FuncAsIFunction<ArrayQuery<T>, bool>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>>(this, new FuncAsIFunction<ArrayQuery<T>, bool>(@operator));
	}
}