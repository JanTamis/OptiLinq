using OptiLinq.Interfaces;

namespace OptiLinq;

public struct AppendEnumerator<T, TBaseEnumerator> : IOptiEnumerator<T> where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private bool _hasAppended;
	private readonly T _element;
	private TBaseEnumerator _baseEnumerator;

	internal AppendEnumerator(TBaseEnumerator baseEnumerator, T element)
	{
		_baseEnumerator = baseEnumerator;
		_element = element;
		_hasAppended = false;
	}

	public T Current => _hasAppended ? _element : _baseEnumerator.Current;

	public bool MoveNext()
	{
		if (_hasAppended)
		{
			return false;
		}

		if (_baseEnumerator.MoveNext())
		{
			return true;
		}

		_hasAppended = true;
		return true;
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}