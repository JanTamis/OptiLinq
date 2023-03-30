using System.Numerics;

namespace OptiLinq;

public partial struct IListQuery<T>
{
	public TakeQuery<TCount, T, IListQuery<T>, IListEnumerator<T>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, IListQuery<T>, IListEnumerator<T>>(ref this, count);
	}
}