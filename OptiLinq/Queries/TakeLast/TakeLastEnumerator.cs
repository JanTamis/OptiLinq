using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct TakeLastEnumerator<T, TBaseEnumerator> : IOptiEnumerator<T>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;
	private readonly int _count;
	private bool _isInitialized = false;

	private PooledQueue<T> _queue;

	public TakeLastEnumerator(TBaseEnumerator baseEnumerator, int count)
	{
		_baseEnumerator = baseEnumerator;
		_count = count;
		_queue = new PooledQueue<T>(count);
	}

	public T Current { get; private set; } = default!;

	public bool MoveNext()
	{
		if (!_isInitialized)
		{
			_isInitialized = true;

			while (_baseEnumerator.MoveNext())
			{
				if (_queue.Count == _count)
				{
					_queue.Dequeue();
				}

				_queue.Enqueue(_baseEnumerator.Current);
			}
		}

		if (_queue.Count > 0)
		{
			Current = _queue.Dequeue();
			return true;
		}

		return false;
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
		_queue.Dispose();
	}
}