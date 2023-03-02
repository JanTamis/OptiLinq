using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
{
	public SelectManyQuery<ArrayQuery<T>, TResult, TOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, IOptiQuery<TResult>> SelectMany<TOperator, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<ArrayQuery<T>, IOptiQuery<TResult>>
	{
		return new SelectManyQuery<ArrayQuery<T>, TResult, TOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, IOptiQuery<TResult>>(ref this, @operator);
	}

	public SelectManyQuery<ArrayQuery<T>, TResult, FuncAsIFunction<ArrayQuery<T>, IOptiQuery<TResult>>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, IOptiQuery<TResult>> SelectMany<TResult>(Func<ArrayQuery<T>, IOptiQuery<TResult>> @operator)
	{
		return new SelectManyQuery<ArrayQuery<T>, TResult, FuncAsIFunction<ArrayQuery<T>, IOptiQuery<TResult>>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, IOptiQuery<TResult>>(ref this, new FuncAsIFunction<ArrayQuery<T>, IOptiQuery<TResult>>(@operator));
	}

	public SelectManyQuery<ArrayQuery<T>, TResult, TOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TSubQuery> SelectMany<TOperator, TSubQuery, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<ArrayQuery<T>, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<ArrayQuery<T>, TResult, TOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<ArrayQuery<T>, TResult, FuncAsIFunction<ArrayQuery<T>, TSubQuery>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TSubQuery> SelectMany<TSubQuery, TResult>(Func<ArrayQuery<T>, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<ArrayQuery<T>, TResult, FuncAsIFunction<ArrayQuery<T>, TSubQuery>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TSubQuery>(ref this, new FuncAsIFunction<ArrayQuery<T>, TSubQuery>(@operator));
	}
}