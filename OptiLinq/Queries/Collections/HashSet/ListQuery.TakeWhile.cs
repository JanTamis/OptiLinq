using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public TakeWhileQuery<T, TOperator, HashSetQuery<T>, HashSet<T>.Enumerator> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T, bool>
	{
		return new TakeWhileQuery<T, TOperator, HashSetQuery<T>, HashSet<T>.Enumerator>(this, @operator);
	}

	public TakeWhileQuery<T, FuncAsIFunction<T, bool>, HashSetQuery<T>, HashSet<T>.Enumerator> TakeWhile(Func<T, bool> @operator)
	{
		return new TakeWhileQuery<T, FuncAsIFunction<T, bool>, HashSetQuery<T>, HashSet<T>.Enumerator>(this, new FuncAsIFunction<T, bool>(@operator));
	}
}