using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public ExceptQuery<T, EqualityComparer<T>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TComparer, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TComparer, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TOtherQuery>(this, other, comparer);
	}
}