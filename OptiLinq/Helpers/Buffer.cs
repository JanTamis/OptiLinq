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
	internal readonly TElement[]? _items;

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
		_items = source.ToArray(out _count);
	}
}