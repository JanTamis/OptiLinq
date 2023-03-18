using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct RandomEnumerator : IEnumerator<int>
{
	private readonly Random _random;

	public RandomEnumerator(Random random)
	{
		_random = random;
	}

	object IEnumerator.Current { get; }
	
	public int Current { get; private set; }

	public bool MoveNext()
	{
		Current = _random.Next();
		return true;
	}

	public void Reset()
	{
		throw new NotSupportedException();
	}

	public void Dispose()
	{
	}
}