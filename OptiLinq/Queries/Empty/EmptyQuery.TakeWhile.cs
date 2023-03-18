using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> TakeWhile<TOperator>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, bool>
	{
		return this;
	}

	public EmptyQuery<T> TakeWhile(Func<T, bool> @operator)
	{
		return this;
	}
}