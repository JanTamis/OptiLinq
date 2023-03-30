using System.Collections;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct BreadthFirstEnumerator<T, TSelector, TSelectorEnumerable> : IEnumerator<T>
	where TSelector : struct, IFunction<T, TSelectorEnumerable>
	where TSelectorEnumerable : IEnumerable<T>
{
	private PooledQueue<T> _queue;
	private TSelector _selector;
	private readonly T _root;

	public BreadthFirstEnumerator(TSelector selector, T root)
	{
		_selector = selector;
		_root = root;

		_queue = new PooledQueue<T>();
		_queue.Enqueue(root);
	}


	public T Current { get; private set; }

	object IEnumerator.Current => Current;

	public bool MoveNext()
	{
		if (_queue.Count != 0)
		{
			Current = _queue.Dequeue();

			foreach (var item in _selector.Eval(Current))
			{
				_queue.Enqueue(item);
			}

			return true;
		}

		return false;
	}

	public void Reset()
	{
		_queue.Clear();
		_queue.Enqueue(_root);
	}

	public void Dispose()
	{
		_queue.Dispose();
	}
}