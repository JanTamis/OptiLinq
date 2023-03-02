using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct AppendQuery<T, TBaseQuery, TBaseEnumerator>
{
	public WhereQuery<T, TWhereOperator, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}