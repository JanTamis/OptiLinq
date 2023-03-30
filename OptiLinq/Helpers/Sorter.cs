using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Interfaces;

namespace OptiLinq.Collections;

public static class Sorter<T, TSorter> where TSorter : IOptiComparer<T>
{
	public static void Sort(Span<T> keys, in TSorter comparer)
	{
		IntrospectiveSort(keys, in comparer);
	}

	public static int BinarySearch(Span<T> array, int index, int length, in T value, in TSorter comparer)
	{
		return InternalBinarySearch(array, index, length, in value, comparer);
	}

	internal static int InternalBinarySearch(Span<T> array, int index, int length, in T value, in TSorter comparer)
	{
		var lo = index;
		var hi = index + length - 1;

		while (lo <= hi)
		{
			var i = lo + ((hi - lo) >> 1);
			var order = comparer.Compare(in array[i], in value);

			switch (order)
			{
				case 0:
					return i;
				case < 0:
					lo = i + 1;
					break;
				default:
					hi = i - 1;
					break;
			}
		}

		return ~lo;
	}

	private static void SwapIfGreater(Span<T> keys, in TSorter comparer, int i, int j)
	{
		if (comparer.Compare(in keys[i], in keys[j]) > 0)
		{
			(keys[i], keys[j]) = (keys[j], keys[i]);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void Swap(Span<T> a, int i, int j)
	{
		(a[i], a[j]) = (a[j], a[i]);
	}

	internal static void IntrospectiveSort(Span<T> keys, in TSorter comparer)
	{
		if (keys.Length > 1)
		{
			IntroSort(keys, 2 * (BitOperations.Log2((uint)keys.Length) + 1), in comparer);
		}
	}

	private static void IntroSort(Span<T> keys, int depthLimit, in TSorter comparer)
	{
		var partitionSize = keys.Length;

		while (partitionSize > 1)
		{
			if (partitionSize <= 16)
			{
				switch (partitionSize)
				{
					case 2:
						SwapIfGreater(keys, in comparer, 0, 1);
						return;
					case 3:
						SwapIfGreater(keys, in comparer, 0, 1);
						SwapIfGreater(keys, in comparer, 0, 2);
						SwapIfGreater(keys, in comparer, 1, 2);
						return;
					default:
						InsertionSort(keys.Slice(0, partitionSize), in comparer);
						return;
				}
			}

			if (depthLimit == 0)
			{
				HeapSort(keys.Slice(0, partitionSize), in comparer);
				return;
			}

			depthLimit--;

			var p = PickPivotAndPartition(keys.Slice(0, partitionSize), in comparer);

			// Note we've already partitioned around the pivot and do not have to move the pivot again.
			IntroSort(keys[(p + 1)..partitionSize], depthLimit, in comparer);
			partitionSize = p;
		}
	}

	private static int PickPivotAndPartition(Span<T> keys, in TSorter comparer)
	{
		var hi = keys.Length - 1;

		// Compute median-of-three.  But also partition them, since we've done the comparison.
		var middle = hi >> 1;

		// Sort lo, mid and hi appropriately, then pick mid as the pivot.
		SwapIfGreater(keys, in comparer, 0, middle); // swap the low with the mid point
		SwapIfGreater(keys, in comparer, 0, hi); // swap the low with the high
		SwapIfGreater(keys, in comparer, middle, hi); // swap the middle with the high

		var pivot = keys[middle];
		Swap(keys, middle, hi - 1);
		int left = 0, right = hi - 1; // We already partitioned lo and hi and put the pivot in hi - 1.  And we pre-increment & decrement below.

		while (left < right)
		{
			while (comparer.Compare(in keys[++left], in pivot) < 0) ;
			while (comparer.Compare(in pivot, in keys[--right]) < 0) ;

			if (left >= right)
				break;

			Swap(keys, left, right);
		}

		// Put pivot in the right location.
		if (left != hi - 1)
		{
			Swap(keys, left, hi - 1);
		}

		return left;
	}

	private static void HeapSort(Span<T> keys, in TSorter comparer)
	{
		var n = keys.Length;

		for (var i = n >> 1; i >= 1; i--)
		{
			DownHeap(keys, i, n, in comparer);
		}

		for (var i = n; i > 1; i--)
		{
			Swap(keys, 0, i - 1);
			DownHeap(keys, 1, i - 1, in comparer);
		}
	}

	private static void DownHeap(Span<T> keys, int i, int n, in TSorter comparer)
	{
		var d = keys[i - 1];

		while (i <= n >> 1)
		{
			var child = 2 * i;

			if (child < n && comparer.Compare(in keys[child - 1], in keys[child]) < 0)
			{
				child++;
			}

			if (!(comparer.Compare(in d, in keys[child - 1]) < 0))
				break;

			keys[i - 1] = keys[child - 1];
			i = child;
		}

		keys[i - 1] = d;
	}

	private static void InsertionSort(Span<T> keys, in TSorter comparer)
	{
		for (var i = 0; i < keys.Length - 1; i++)
		{
			var t = keys[i + 1];
			var j = i;

			while (j >= 0 && comparer.Compare(in t, in keys[j]) < 0)
			{
				keys[j + 1] = keys[j];
				j--;
			}

			keys[j + 1] = t;
		}
	}
}