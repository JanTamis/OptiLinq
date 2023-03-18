using System.Collections;

namespace OptiLinq;

public struct AppendEnumerator<T, TBaseEnumerator> : IEnumerator<T> where TBaseEnumerator : IEnumerator<T>
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


	object IEnumerator.Current => Current;

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

	public void Reset()
	{
		_baseEnumerator.Reset();
		_hasAppended = false;
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}