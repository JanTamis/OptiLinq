using System.Numerics;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public TakeQuery<TCount, T, ListQuery<T>, List<T>.Enumerator> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, ListQuery<T>, List<T>.Enumerator>(ref this, count);
	}
}