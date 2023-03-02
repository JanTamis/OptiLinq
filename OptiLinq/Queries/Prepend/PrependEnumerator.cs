using OptiLinq.Interfaces;

namespace OptiLinq;

public struct PrependEnumerator<T, TBaseEnumerator> : IOptiEnumerator<T> where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private bool _hasAppended;
	private readonly T _element;
	private TBaseEnumerator _baseEnumerator;

	internal PrependEnumerator(TBaseEnumerator baseEnumerator, T element)
	{
		_baseEnumerator = baseEnumerator;
		_element = element;
		_hasAppended = false;
	}

	public T Current => _hasAppended ? _baseEnumerator.Current : _element;

	public bool MoveNext()
	{
		if (_hasAppended)
		{
			return _baseEnumerator.MoveNext();
		}

		_hasAppended = true;
		return true;
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}