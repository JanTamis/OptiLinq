using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
{
	public ZipQuery<ArrayQuery<T>, TResult, TOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery> Zip<TResult, TOperator, TOtherQuery>(in TOtherQuery other, TOperator @operator = default)
		where TOperator : struct, IFunction<ArrayQuery<T>, ArrayQuery<T>, TResult>
		where TOtherQuery : struct, IOptiQuery<ArrayQuery<T>>
	{
		return new ZipQuery<ArrayQuery<T>, TResult, TOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery>(@operator, in this, in other);
	}

	public ZipQuery<ArrayQuery<T>, TResult, FuncAsIFunction<ArrayQuery<T>, ArrayQuery<T>, TResult>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery> Zip<TResult, TOtherQuery>(in TOtherQuery other, Func<ArrayQuery<T>, ArrayQuery<T>, TResult> @operator)
		where TOtherQuery : struct, IOptiQuery<ArrayQuery<T>>
	{
		return new ZipQuery<ArrayQuery<T>, TResult, FuncAsIFunction<ArrayQuery<T>, ArrayQuery<T>, TResult>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery>(new FuncAsIFunction<ArrayQuery<T>, ArrayQuery<T>, TResult>(@operator), in this, in other);
	}
}