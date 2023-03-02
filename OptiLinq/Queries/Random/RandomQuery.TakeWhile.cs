using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RandomQuery
{
	public TakeWhileQuery<int, TOperator, RandomQuery, RandomEnumerator> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<int, bool>
	{
		return new TakeWhileQuery<int, TOperator, RandomQuery, RandomEnumerator>(this, @operator);
	}

	public TakeWhileQuery<int, FuncAsIFunction<int, bool>, RandomQuery, RandomEnumerator> TakeWhile(Func<int, bool> @operator)
	{
		return new TakeWhileQuery<int, FuncAsIFunction<int, bool>, RandomQuery, RandomEnumerator>(this, new FuncAsIFunction<int, bool>(@operator));
	}
}