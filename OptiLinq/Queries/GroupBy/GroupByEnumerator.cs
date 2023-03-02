using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct GroupByEnumerator<TKey, TElement, TComparer> : IOptiEnumerator<ArrayQuery<TElement>>
	where TComparer : IEqualityComparer<TKey>
{
	private int index = -1;

	private Lookup<TKey, TElement, TComparer> _lookup;

	public GroupByEnumerator(Lookup<TKey, TElement, TComparer> lookup)
	{
		_lookup = lookup;
	}

	public ArrayQuery<TElement> Current { get; private set; }

	public bool MoveNext()
	{
		if (index + 1 < _lookup.Count)
		{
			index++;

			Current = new ArrayQuery<TElement>(_lookup.slots[index].value.ToArray());

			return true;
		}

		return false;
	}

	public void Dispose()
	{
		_lookup.Dispose();
	}
}