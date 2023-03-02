using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> GroupBy<TKey, TKeySelector, TComparer>(TKeySelector keySelector = default, TComparer comparer = default)
		where TKeySelector : struct, IFunction<T, TKey>
		where TComparer : IEqualityComparer<TKey>
	{
		return this;
	}

	public EmptyQuery<T> GroupBy<TKey, TKeySelector>(TKeySelector keySelector)
		where TKeySelector : struct, IFunction<T, TKey>
	{
		return this;
	}

	public EmptyQuery<T> GroupBy<TKey>(Func<T, TKey> keySelector)
	{
		return this;
	}

	public EmptyQuery<T> GroupBy<TKey, TComparer>(Func<T, TKey> keySelector, TComparer comparer = default)
		where TComparer : IEqualityComparer<TKey>
	{
		return this;
	}

	public EmptyQuery<T> GroupBy<TKey>(Func<T, TKey> keySelector, EqualityComparer<TKey> comparer)
	{
		return this;
	}
}