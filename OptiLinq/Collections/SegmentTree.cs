using System.Buffers;
using System.Runtime.CompilerServices;

namespace OptiLinq.Collections;

/// <summary>
/// Represents a strongly typed list of objects that is very fast to aggregate.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public struct SegmentTree<T> : IDisposable
{
	private const string ST_IsFixedSize = "Size of SegmentTree cannot be changed. This method will throw a NotSupportedException.";

	private readonly RefAggregator _aggregator;
	private readonly T[] _storage;
	private readonly int _start, _length;
	private readonly T _basis;

	/// <summary>
	/// Initializes a new instance of the <see cref="SegmentTree{T}"/> class that has specified length and filled with <paramref name="basis"/>,
	/// optionally copying values from <paramref name="original"/> source.
	/// </summary>
	/// <param name="length">The number of elements that the new segment tree can store.</param>
	/// <param name="aggregator">
	/// Function used to aggregate elements.<br/>
	/// <b>THIS FUNCTION MUST MEET SEVERAL CONDITIONS, see remarks.</b>
	/// </param>
	/// <param name="original">Source to copy values from.</param>
	/// <param name="basis">The value that is initial for the elements and neutral for the aggregator function.</param>
	/// <remarks>
	/// Actual memory consumption is O(2*P), where P is the smallest power of two not smaller than <paramref name="length"/>
	/// <br/><br/>
	/// <paramref name="aggregator"/> function should meet these conditions 
	/// ('<c>G</c>' is <paramref name="aggregator"/>, '<c>B</c>' is <paramref name="basis"/>, 
	/// '<c>E</c>', '<c>F</c>' and '<c>H</c>' are arbitrary instances of <typeparamref name="T"/> among the assumed range): <br/>
	/// <code>
	/// G(B, E) == E
	/// G(E, F) == G(F, E)
	/// G(E, G(F, H)) == G(F, G(E, H))
	/// </code>
	/// <b>If these conditions are violated, the BEHAVIOR of the <see cref="Aggregate(Range)"/> method is UNDEFINED.</b>
	/// </remarks>
	public SegmentTree(int length, RefAggregator aggregator, ReadOnlySpan<T> original, T basis = default!)
	{
		_basis = basis;
		_length = length;
		_aggregator = aggregator;

		var p = MinPow2(length);

		_storage = ArrayPool<T>.Shared.Rent(1 << (p + 1));
		_start = _storage.Length / 2;
		InitializeStorage(original);
	}

	private void InitializeStorage(ReadOnlySpan<T> original)
	{
		var index = 0;

		while (index < _length && index < original.Length)
			_storage[_start + index] = original[index++];
		while (index < _length)
			_storage[_start + index++] = _basis;

		AggregateImpl(ref _storage[_start - 1], ref _storage[1], ref _storage[^1]);
	}

	public T Aggregate(Range at)
	{
		var (left, len) = at.GetOffsetAndLength(_length);
		left += _start;
		var right = left + len - 1;
		if (left < _start || right >= _start + _length) throw new ArgumentOutOfRangeException(nameof(at));
		// Now 'left' and 'right' are the beginning and end (inclusive)
		// of the range for aggregation in the '_storage' indexing.

		// Use direct ref to suppres bounds checking since we have checked indexes
		ref var storStart = ref _storage[0];
		var ret = _basis;
		while (left <= right)
		{
			if ((left & 1) == 1) _aggregator(in ret, in Unsafe.Add(ref storStart, left), out ret);
			if ((right & 1) == 0) _aggregator(in ret, in Unsafe.Add(ref storStart, right), out ret);

			left = (left + 1) >> 1;
			right = (right - 1) >> 1;
		}

		return ret;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void AggregateImpl(ref T head, ref T rightStop, ref T rightChild)
	{
		ref var leftChild = ref Unsafe.Subtract(ref rightChild, 1);

		while (!Unsafe.AreSame(ref rightStop, ref rightChild))
		{
			_aggregator(in leftChild, in rightChild, out head);
			rightChild = ref Unsafe.Subtract(ref rightChild, 2);
			leftChild = ref Unsafe.Subtract(ref leftChild, 2)!;
			head = ref Unsafe.Subtract(ref head, 1);
		}
	}

	private static int MinPow2(int n)
	{
		int p = 0, a = 1;
		while (a < n)
		{
			a *= 2;
			p++;
		}

		return p;
	}

	/// <summary>
	/// Represents a method that aggregates two data values into one using references.<br/>
	/// <b>It is possible that <c>a==result</c> or <c>b==result</c> (points to the same location)</b>
	/// </summary>
	/// <typeparam name="T">Type of data to aggregate.</typeparam>
	/// <param name="a">A reference to the first instance.</param>
	/// <param name="b">A reference to the second instance.</param>
	/// <param name="result">A reference to the result.</param>
	public delegate void RefAggregator(in T a, in T b, out T result);

	public void Dispose()
	{
		ArrayPool<T>.Shared.Return(_storage, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
	}
}