using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public UnionQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, TOtherQuery, EqualityComparer<T>> Union<TOtherQuery>(TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new UnionQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, TOtherQuery, EqualityComparer<T>>(ref this, ref other, EqualityComparer<T>.Default);
	}

	public UnionQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, TOtherQuery, TComparer> Union<TOtherQuery, TComparer>(TOtherQuery other, TComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new UnionQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, TOtherQuery, TComparer>(ref this, ref other, comparer);
	}
}