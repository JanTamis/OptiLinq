using OptiLinq.Interfaces;

namespace OptiLinq.Helpers;

/// <summary>
/// A buffer into which the contents of an <see cref="IEnumerable{TElement}"/> can be stored.
/// </summary>
/// <typeparam name="TElement">The type of the buffer's elements.</typeparam>
internal readonly struct Buffer<TElement, TQuery, TEnumerable>
	where TEnumerable : struct, IOptiEnumerator<TElement>
	where TQuery : struct, IOptiQuery<TElement, TEnumerable>
{
	/// <summary>
	/// The stored items.
	/// </summary>
	internal readonly TElement[] _items;

	/// <summary>
	/// The number of stored items.
	/// </summary>
	internal readonly int _count;

	/// <summary>
	/// Fully enumerates the provided enumerable and stores its items into an array.
	/// </summary>
	/// <param name="source">The enumerable to be store.</param>
	internal Buffer(TQuery source)
	{
		_items = source.ToArray();
	}

	/// <summary>Converts an enumerable to an array using the same logic as List{T}.</summary>
	/// <param name="source">The enumerable to convert.</param>
	/// <param name="length">The number of items stored in the resulting array, 0-indexed.</param>
	/// <returns>
	/// The resulting array.  The length of the array may be greater than <paramref name="length"/>,
	/// which is the actual number of elements in the array.
	/// </returns>
	internal static TElement[] ToArray(TQuery source, out int length)
	{
		using (var en = source.GetEnumerator())
		{
			if (en.MoveNext())
			{
				const int DefaultCapacity = 4;
				
				var arr = new TElement[DefaultCapacity];
				
				arr[0] = en.Current;
				var count = 1;

				while (en.MoveNext())
				{
					if (count == arr.Length)
					{
						// This is the same growth logic as in List<T>:
						// If the array is currently empty, we make it a default size.  Otherwise, we attempt to
						// double the size of the array.  Doubling will overflow once the size of the array reaches
						// 2^30, since doubling to 2^31 is 1 larger than Int32.MaxValue.  In that case, we instead
						// constrain the length to be Array.MaxLength (this overflow check works because of the
						// cast to uint).
						var newLength = count << 1;
						if ((uint)newLength > Array.MaxLength)
						{
							newLength = Array.MaxLength <= count ? count + 1 : Array.MaxLength;
						}

						Array.Resize(ref arr, newLength);
					}

					arr[count++] = en.Current;
				}

				length = count;
				return arr;
			}
		}

		length = 0;
		return Array.Empty<TElement>();
	}
}