using System.Numerics;

namespace OptiLinq;

public partial struct IListQuery<T>
{
	public SkipQuery<TCount, T, IListQuery<T>, IListEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, IListQuery<T>, IListEnumerator<T>>(ref this, count);
	}
}