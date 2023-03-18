using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public UnionQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TOtherQuery, EqualityComparer<T>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new UnionQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TOtherQuery, EqualityComparer<T>>(ref this, ref other, EqualityComparer<T>.Default);
	}

	public UnionQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TOtherQuery, TComparer> Union<TOtherQuery, TComparer>(TOtherQuery other, TComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new UnionQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TOtherQuery, TComparer>(ref this, ref other, comparer);
	}
}