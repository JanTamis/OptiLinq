using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public UnionQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, TOtherQuery, EqualityComparer<T>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new UnionQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, TOtherQuery, EqualityComparer<T>>(ref this, ref other, EqualityComparer<T>.Default);
	}

	public UnionQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, TOtherQuery, TComparer> Union<TOtherQuery, TComparer>(TOtherQuery other, TComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new UnionQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>, TOtherQuery, TComparer>(ref this, ref other, comparer);
	}
}