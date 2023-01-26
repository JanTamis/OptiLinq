using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ListEnumerator<T> : IOptiEnumerator<T>
{
	private readonly IList<T> _list;
	private int index = -1;

	internal ListEnumerator(IList<T> list)
	{
		_list = list;
	}

	public T Current => _list[Math.Min(index, _list.Count)];

	public bool MoveNext()
	{
		index++;
		return index < _list.Count;
	}
	
	public void Dispose()
	{
		
	}
}