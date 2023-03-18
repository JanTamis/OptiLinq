namespace OptiLinq.Collections;

public ref struct LookupEnumerator<TKey, TValue>
{
	private readonly ref Slot<TKey, TValue> slot;

	private int index = -1;

	public TValue Current => slot.value[index];

	internal LookupEnumerator(ref Slot<TKey, TValue> slot)
	{
		this.slot = ref slot;
	}

	public bool MoveNext()
	{
		if (slot.hashCode != -1 && index + 1 < slot.value.Count)
		{
			index++;
			return true;
		}

		return false;
	}
}