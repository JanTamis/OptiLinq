using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public TakeWhileQuery<T, TOtherOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> TakeWhile<TOtherOperator>(TOtherOperator @operator)
		where TOtherOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOtherOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}