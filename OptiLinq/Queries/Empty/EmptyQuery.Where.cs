using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return this;
	}

	public EmptyQuery<T> Where(Func<T, bool> @operator)
	{
		return this;
	}
}