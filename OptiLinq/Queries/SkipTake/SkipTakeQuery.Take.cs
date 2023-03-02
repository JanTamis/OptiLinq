using System.Numerics;

namespace OptiLinq;

public partial struct SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>
{
	public TakeQuery<TCount, T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(ref this, count);
	}

	public SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator> Take(TTakeCount count)
	{
		return new SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>(ref _baseEnumerable, _skipCount, TTakeCount.Min(_takeCount, count));
	}
}