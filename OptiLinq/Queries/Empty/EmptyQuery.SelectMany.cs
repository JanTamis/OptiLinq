using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<TResult> SelectMany<TOperator, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, IOptiQuery<TResult>>
	{
		return new EmptyQuery<TResult>();
	}

	public EmptyQuery<TResult> SelectMany<TResult>(Func<T, IOptiQuery<TResult>> @operator)
	{
		return new EmptyQuery<TResult>();
	}

	public EmptyQuery<TResult> SelectMany<TOperator, TSubQuery, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new EmptyQuery<TResult>();
	}

	public EmptyQuery<TResult> SelectMany<TSubQuery, TResult>(Func<T, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new EmptyQuery<TResult>();
	}
}