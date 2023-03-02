using System.Numerics;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public TakeQuery<TCount, T, ListQuery<T>, ListEnumerator<T>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, ListQuery<T>, ListEnumerator<T>>(ref this, count);
	}
}