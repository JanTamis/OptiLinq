using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
{
	public WhereQuery<ArrayQuery<T>, TWhereOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<ArrayQuery<T>, bool>
	{
		return new WhereQuery<ArrayQuery<T>, TWhereOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>>(ref this, @operator);
	}

	public WhereQuery<ArrayQuery<T>, FuncAsIFunction<ArrayQuery<T>, bool>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>> Where(Func<ArrayQuery<T>, bool> @operator)
	{
		return new WhereQuery<ArrayQuery<T>, FuncAsIFunction<ArrayQuery<T>, bool>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>>(ref this, new FuncAsIFunction<ArrayQuery<T>, bool>(@operator));
	}
}