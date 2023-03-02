using System.Numerics;

namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return this;
	}
}