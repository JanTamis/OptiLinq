using System.Diagnostics;
using OptiLinq.Interfaces;

namespace OptiLinq.Collections;

internal abstract class EnumerableSorter<TElement>
{
	internal EnumerableSorter<TElement>? _next;
	
	/// <summary>Function that returns its input unmodified.</summary>
	/// <remarks>
	/// Used for reference equality in order to avoid unnecessary computation when a caller
	/// can benefit from knowing that the produced value is identical to the input.
	/// </remarks>
	internal abstract void ComputeKeys(ref PooledList<TElement> elements, int count);

	internal abstract int CompareAnyKeys(int index1, int index2);

	private PooledList<int> ComputeMap(ref PooledList<TElement> elements, int count)
	{
		ComputeKeys(ref elements, count);

		var map = new PooledList<int>(count);

		for (var i = 0; i < count; i++)
		{
			map.Add(i);
		}
		
		return map;
	}

	internal PooledList<int> Sort(ref PooledList<TElement> elements, int count)
	{
		var map = ComputeMap(ref elements, count);

		QuickSort(ref map, 0, count - 1);
		return map;
	}

	internal PooledList<int> Sort(ref PooledList<TElement> elements, int count, int minIdx, int maxIdx)
	{
		var map = ComputeMap(ref elements, count);
		PartialQuickSort(ref map, 0, count - 1, minIdx, maxIdx);
		return map;
	}

	internal TElement ElementAt(ref PooledList<TElement> elements, int count, int idx)
	{
		var map = ComputeMap(ref elements, count);

		using (map)
		{
			return idx == 0
				? elements[Min(ref map, count)]
				: elements[QuickSelect(ref map, count - 1, idx)];
		}
	}

	protected abstract void QuickSort(ref PooledList<int> map, int left, int right);

	// Sorts the k elements between minIdx and maxIdx without sorting all elements
	// Time complexity: O(n + k log k) best and average case. O(n^2) worse case.
	protected abstract void PartialQuickSort(ref PooledList<int> map, int left, int right, int minIdx, int maxIdx);

	// Finds the element that would be at idx if the collection was sorted.
	// Time complexity: O(n) best and average case. O(n^2) worse case.
	protected abstract int QuickSelect(ref PooledList<int> map, int right, int idx);

	protected abstract int Min(ref PooledList<int> map, int count);
}

internal sealed class EnumerableSorter<TElement, TComparer> : EnumerableSorter<TElement>
	where TComparer : IComparer<TElement>
{
	private readonly TComparer _comparer;
	private readonly bool _descending;
	private TElement[] _keys;

	internal EnumerableSorter(TComparer comparer, bool descending, EnumerableSorter<TElement>? next)
	{
		_comparer = comparer;
		_descending = descending;
		_next = next;
	}

	internal override void ComputeKeys(ref PooledList<TElement> elements, int count)
	{
		_keys = elements.ToArray();

		_next?.ComputeKeys(ref elements, count);
	}

	internal override int CompareAnyKeys(int index1, int index2)
	{
		var keys = _keys;
		Debug.Assert(keys != null);

		var c = _comparer.Compare(keys[index1], keys[index2]);
		if (c == 0)
		{
			if (_next == null)
			{
				return index1 - index2; // ensure stability of sort
			}

			return _next.CompareAnyKeys(index1, index2);
		}

		// -c will result in a negative value for int.MinValue (-int.MinValue == int.MinValue).
		// Flipping keys earlier is more likely to trigger something strange in a comparer,
		// particularly as it comes to the sort being stable.
		return (_descending != (c > 0)) ? 1 : -1;
	}

	private int CompareAnyKeys_DefaultComparer_NoNext_Ascending(int index1, int index2)
	{
		Debug.Assert(typeof(TElement).IsValueType);
		Debug.Assert(_next is null);
		Debug.Assert(!_descending);

		var keys = _keys;
		Debug.Assert(keys != null);

		var c = Comparer<TElement>.Default.Compare(keys[index1], keys[index2]);
		return
			c == 0
				? index1 - index2
				: // ensure stability of sort
				c;
	}

	private int CompareAnyKeys_DefaultComparer_NoNext_Descending(int index1, int index2)
	{
		Debug.Assert(typeof(TElement).IsValueType);
		Debug.Assert(_next is null);
		Debug.Assert(_descending);

		var keys = _keys;
		Debug.Assert(keys != null);

		var c = Comparer<TElement>.Default.Compare(keys[index2], keys[index1]);
		return
			c == 0
				? index1 - index2
				: // ensure stability of sort
				c;
	}

	private int CompareKeys(int index1, int index2) => index1 == index2 ? 0 : CompareAnyKeys(index1, index2);

	protected override void QuickSort(ref PooledList<int> keys, int lo, int hi)
	{
		Comparison<int> comparison;

		if (typeof(TElement).IsValueType && _next is null && _comparer.Equals(Comparer<TElement>.Default))
		{
			// We can use Comparer<TKey>.Default.Compare and benefit from devirtualization and inlining.
			// We can also avoid extra steps to check whether we need to deal with a subsequent tie breaker (_next).
			if (!_descending)
			{
				comparison = CompareAnyKeys_DefaultComparer_NoNext_Ascending;
			}
			else
			{
				comparison = CompareAnyKeys_DefaultComparer_NoNext_Descending;
			}
		}
		else
		{
			comparison = CompareAnyKeys;
		}

		keys.AsSpan().Slice(lo, hi - lo + 1).Sort(comparison);
	}

	// Sorts the k elements between minIdx and maxIdx without sorting all elements
	// Time complexity: O(n + k log k) best and average case. O(n^2) worse case.
	protected override void PartialQuickSort(ref PooledList<int> map, int left, int right, int minIdx, int maxIdx)
	{
		do
		{
			var i = left;
			var j = right;
			var x = map[i + ((j - i) >> 1)];
			do
			{
				while (i < map.Count && CompareKeys(x, map[i]) > 0)
				{
					i++;
				}

				while (j >= 0 && CompareKeys(x, map[j]) < 0)
				{
					j--;
				}

				if (i > j)
				{
					break;
				}

				if (i < j)
				{
					(map[i], map[j]) = (map[j], map[i]);
				}

				i++;
				j--;
			} while (i <= j);

			if (minIdx >= i)
			{
				left = i + 1;
			}
			else if (maxIdx <= j)
			{
				right = j - 1;
			}

			if (j - left <= right - i)
			{
				if (left < j)
				{
					PartialQuickSort(ref map, left, j, minIdx, maxIdx);
				}

				left = i;
			}
			else
			{
				if (i < right)
				{
					PartialQuickSort(ref map, i, right, minIdx, maxIdx);
				}

				right = j;
			}
		} while (left < right);
	}

	// Finds the element that would be at idx if the collection was sorted.
	// Time complexity: O(n) best and average case. O(n^2) worse case.
	protected override int QuickSelect(ref PooledList<int> map, int right, int idx)
	{
		var left = 0;
		do
		{
			var i = left;
			var j = right;
			var x = map[i + ((j - i) >> 1)];
			do
			{
				while (i < map.Count && CompareKeys(x, map[i]) > 0)
				{
					i++;
				}

				while (j >= 0 && CompareKeys(x, map[j]) < 0)
				{
					j--;
				}

				if (i > j)
				{
					break;
				}

				if (i < j)
				{
					(map[i], map[j]) = (map[j], map[i]);
				}

				i++;
				j--;
			} while (i <= j);

			if (i <= idx)
			{
				left = i + 1;
			}
			else
			{
				right = j - 1;
			}

			if (j - left <= right - i)
			{
				if (left < j)
				{
					right = j;
				}

				left = i;
			}
			else
			{
				if (i < right)
				{
					left = i;
				}

				right = j;
			}
		} while (left < right);

		return map[idx];
	}

	protected override int Min(ref PooledList<int> map, int count)
	{
		var index = 0;
		for (var i = 1; i < count; i++)
		{
			if (CompareKeys(map[i], map[index]) < 0)
			{
				index = i;
			}
		}

		return map[index];
	}
}

internal sealed class EnumerableSorter<TElement, TKey, TKeySelector, TComparer> : EnumerableSorter<TElement>
	where TKeySelector : IFunction<TElement, TKey>
	where TComparer : IComparer<TKey>
{
	private TKeySelector _keySelector;
	private TComparer _comparer;
	private readonly bool _descending;
	private TKey[]? _keys;

	internal EnumerableSorter(TKeySelector keySelector, TComparer comparer, bool descending, EnumerableSorter<TElement>? next)
	{
		_keySelector = keySelector;
		_comparer = comparer;
		_descending = descending;
		_next = next;
	}

	internal override void ComputeKeys(ref PooledList<TElement> elements, int count)
	{
		var keys = new TKey[count];

		for (var i = 0; i < keys.Length; i++)
		{
			keys[i] = _keySelector.Eval(elements[i]);
		}

		_keys = keys;

		_next?.ComputeKeys(ref elements, count);
	}

	internal override int CompareAnyKeys(int index1, int index2)
	{
		var keys = _keys;
		Debug.Assert(keys != null);

		var c = _comparer.Compare(keys[index1], keys[index2]);
		if (c == 0)
		{
			if (_next == null)
			{
				return index1 - index2; // ensure stability of sort
			}

			return _next.CompareAnyKeys(index1, index2);
		}

		// -c will result in a negative value for int.MinValue (-int.MinValue == int.MinValue).
		// Flipping keys earlier is more likely to trigger something strange in a comparer,
		// particularly as it comes to the sort being stable.
		return (_descending != (c > 0)) ? 1 : -1;
	}

	private int CompareAnyKeys_DefaultComparer_NoNext_Ascending(int index1, int index2)
	{
		Debug.Assert(typeof(TKey).IsValueType);
		Debug.Assert(_next is null);
		Debug.Assert(!_descending);

		var keys = _keys;
		Debug.Assert(keys != null);

		var c = Comparer<TKey>.Default.Compare(keys[index1], keys[index2]);
		return
			c == 0
				? index1 - index2
				: // ensure stability of sort
				c;
	}

	private int CompareAnyKeys_DefaultComparer_NoNext_Descending(int index1, int index2)
	{
		Debug.Assert(typeof(TKey).IsValueType);
		Debug.Assert(_next is null);
		Debug.Assert(_descending);

		var keys = _keys;
		Debug.Assert(keys != null);

		var c = Comparer<TKey>.Default.Compare(keys[index2], keys[index1]);
		return
			c == 0
				? index1 - index2
				: // ensure stability of sort
				c;
	}

	private int CompareKeys(int index1, int index2) => index1 == index2 ? 0 : CompareAnyKeys(index1, index2);

	protected override void QuickSort(ref PooledList<int> keys, int lo, int hi)
	{
		Comparison<int> comparison;

		if (typeof(TKey).IsValueType && _next is null && _comparer.Equals(Comparer<TKey>.Default))
		{
			// We can use Comparer<TKey>.Default.Compare and benefit from devirtualization and inlining.
			// We can also avoid extra steps to check whether we need to deal with a subsequent tie breaker (_next).
			if (!_descending)
			{
				comparison = CompareAnyKeys_DefaultComparer_NoNext_Ascending;
			}
			else
			{
				comparison = CompareAnyKeys_DefaultComparer_NoNext_Descending;
			}
		}
		else
		{
			comparison = CompareAnyKeys;
		}

		keys.AsSpan().Slice(lo, hi - lo + 1).Sort(comparison);
	}

	// Sorts the k elements between minIdx and maxIdx without sorting all elements
	// Time complexity: O(n + k log k) best and average case. O(n^2) worse case.
	protected override void PartialQuickSort(ref PooledList<int> map, int left, int right, int minIdx, int maxIdx)
	{
		do
		{
			var i = left;
			var j = right;
			var x = map[i + ((j - i) >> 1)];
			do
			{
				while (i < map.Count && CompareKeys(x, map[i]) > 0)
				{
					i++;
				}

				while (j >= 0 && CompareKeys(x, map[j]) < 0)
				{
					j--;
				}

				if (i > j)
				{
					break;
				}

				if (i < j)
				{
					(map[i], map[j]) = (map[j], map[i]);
				}

				i++;
				j--;
			} while (i <= j);

			if (minIdx >= i)
			{
				left = i + 1;
			}
			else if (maxIdx <= j)
			{
				right = j - 1;
			}

			if (j - left <= right - i)
			{
				if (left < j)
				{
					PartialQuickSort(ref map, left, j, minIdx, maxIdx);
				}

				left = i;
			}
			else
			{
				if (i < right)
				{
					PartialQuickSort(ref map, i, right, minIdx, maxIdx);
				}

				right = j;
			}
		} while (left < right);
	}

	// Finds the element that would be at idx if the collection was sorted.
	// Time complexity: O(n) best and average case. O(n^2) worse case.
	protected override int QuickSelect(ref PooledList<int> map, int right, int idx)
	{
		var left = 0;
		do
		{
			var i = left;
			var j = right;
			var x = map[i + ((j - i) >> 1)];
			do
			{
				while (i < map.Count && CompareKeys(x, map[i]) > 0)
				{
					i++;
				}

				while (j >= 0 && CompareKeys(x, map[j]) < 0)
				{
					j--;
				}

				if (i > j)
				{
					break;
				}

				if (i < j)
				{
					(map[i], map[j]) = (map[j], map[i]);
				}

				i++;
				j--;
			} while (i <= j);

			if (i <= idx)
			{
				left = i + 1;
			}
			else
			{
				right = j - 1;
			}

			if (j - left <= right - i)
			{
				if (left < j)
				{
					right = j;
				}

				left = i;
			}
			else
			{
				if (i < right)
				{
					left = i;
				}

				right = j;
			}
		} while (left < right);

		return map[idx];
	}

	protected override int Min(ref PooledList<int> map, int count)
	{
		var index = 0;
		for (var i = 1; i < count; i++)
		{
			if (CompareKeys(map[i], map[index]) < 0)
			{
				index = i;
			}
		}

		return map[index];
	}
}