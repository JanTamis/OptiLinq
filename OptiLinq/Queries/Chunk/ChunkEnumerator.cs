using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ChunkEnumerator<T, TBaseEnumerator> : IOptiEnumerator<T[]>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;
	private readonly int _chunkSize;

	internal ChunkEnumerator(TBaseEnumerator baseEnumerator, int chunkSize)
	{
		_baseEnumerator = baseEnumerator;
		_chunkSize = chunkSize;
	}

	public T[] Current { get; private set; } = null!;

	public bool MoveNext()
	{
		if (_baseEnumerator.MoveNext())
		{
			var arraySize = Math.Min(_chunkSize, 4);
			
			var array = Current is not null
				? GC.AllocateUninitializedArray<T>(Current.Length)
				: GC.AllocateUninitializedArray<T>(arraySize);

			// Store the first item.
			array[0] = _baseEnumerator.Current;
			var i = 1;

			if (_chunkSize != array.Length)
			{
				// This is the first chunk. As we fill the array, grow it as needed.
				for (; i < _chunkSize && _baseEnumerator.MoveNext(); i++)
				{
					if (i >= array.Length)
					{
						var larray = array; // local copy
						
						array = GC.AllocateUninitializedArray<T>((int)Math.Min((uint)_chunkSize, 2 * (uint)array.Length));
						larray.CopyTo(array, 0);
					}

					array[i] = _baseEnumerator.Current;
				}
			}
			else
			{
				// For all but the first chunk, the array will already be correctly sized.
				// We can just store into it until either it's full or MoveNext returns false.

				for (; (uint)i < (uint)array.Length && _baseEnumerator.MoveNext(); i++)
				{
					array[i] = _baseEnumerator.Current;
				}
			}

			if (i != array.Length)
			{
				Array.Resize(ref array, i);
			}

			Current = array;
			return true;
		}
		
		Current = Array.Empty<T>();
		return false;
	}
	
	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}