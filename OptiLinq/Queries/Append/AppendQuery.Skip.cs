using System.Numerics;

namespace OptiLinq;

public partial struct AppendQuery<T, TBaseQuery, TBaseEnumerator>
{
	public SkipQuery<TCount, T, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>>(ref this, count);
	}
}