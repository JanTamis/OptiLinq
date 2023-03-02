using System.Numerics;

namespace OptiLinq;

public partial struct SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public SkipTakeQuery<TCount, TTakeCount, T, TBaseQuery, TBaseEnumerator> Take<TTakeCount>(TTakeCount count)
		where TTakeCount : IBinaryInteger<TTakeCount>
	{
		return new SkipTakeQuery<TCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>(ref _baseEnumerable, _count, count);
	}
}