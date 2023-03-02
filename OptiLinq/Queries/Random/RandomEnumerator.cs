using OptiLinq.Interfaces;

namespace OptiLinq;

public struct RandomEnumerator : IOptiEnumerator<int>
{
	private readonly Random _random;

	public RandomEnumerator(Random random)
	{
		_random = random;
	}

	public int Current { get; private set; }

	public bool MoveNext()
	{
		Current = _random.Next();
		return true;
	}

	public void Dispose()
	{
	}
}