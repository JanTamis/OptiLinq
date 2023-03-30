using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public OrderQuery<T, TBaseQuery, TBaseEnumerator, IFuncComparerWithPrevious<T, TKey, TKeySelector, TComparer>> ThenBy<TKey, TKeySelector>(TKeySelector keySelector = default) where TKeySelector : IFunction<T, TKey>
	{
		return new OrderQuery<T, TBaseQuery, TBaseEnumerator, IFuncComparerWithPrevious<T, TKey, TKeySelector, TComparer>>(ref _baseEnumerable, new IFuncComparerWithPrevious<T, TKey, TKeySelector, TComparer>(keySelector, _comparer));
	}

	public OrderQuery<T, TBaseQuery, TBaseEnumerator, FuncComparerWithPrevious<T, TKey, TComparer>> ThenBy<TKey>(Func<T, TKey> keySelector)
	{
		return new OrderQuery<T, TBaseQuery, TBaseEnumerator, FuncComparerWithPrevious<T, TKey, TComparer>>(ref _baseEnumerable, new FuncComparerWithPrevious<T, TKey, TComparer>(keySelector, _comparer));
	}

	public OrderQuery<T, TBaseQuery, TBaseEnumerator, IFuncComparerWithPreviousDescending<T, TKey, TKeySelector, TComparer>> ThenByDescending<TKey, TKeySelector>(TKeySelector keySelector = default) where TKeySelector : IFunction<T, TKey>
	{
		return new OrderQuery<T, TBaseQuery, TBaseEnumerator, IFuncComparerWithPreviousDescending<T, TKey, TKeySelector, TComparer>>(ref _baseEnumerable, new IFuncComparerWithPreviousDescending<T, TKey, TKeySelector, TComparer>(keySelector, _comparer));
	}

	public OrderQuery<T, TBaseQuery, TBaseEnumerator, FuncComparerWithPreviousDescending<T, TKey, TComparer>> ThenByDescending<TKey>(Func<T, TKey> keySelector)
	{
		return new OrderQuery<T, TBaseQuery, TBaseEnumerator, FuncComparerWithPreviousDescending<T, TKey, TComparer>>(ref _baseEnumerable, new FuncComparerWithPreviousDescending<T, TKey, TComparer>(keySelector, _comparer));
	}
}