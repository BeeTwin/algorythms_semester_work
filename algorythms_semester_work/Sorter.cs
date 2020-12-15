using System;
using System.Collections.Generic;
using System.Linq;

namespace algorythms_semester_work
{
    public static class Sorter
    {
        public static List<T> QuickSort<T>(List<T> array) where T : IComparable
        {
            if (!array.Any())
                return array;
            var stack = new Stack<int>();
            T pivot;
            var pivotIndex = 0;
            var start = pivotIndex + 1;
            var end = array.Count - 1;

            stack.Push(pivotIndex);
            stack.Push(end);

            int startOfSubSet, endOfSubset;

            while (stack.Count > 0)
            {
                endOfSubset = stack.Pop();
                startOfSubSet = stack.Pop();

                start = startOfSubSet + 1;
                pivotIndex = startOfSubSet;
                end = endOfSubset;

                pivot = array[pivotIndex];

                if (start > end)
                    continue;

                while (start < end)
                {
                    while (start <= end && array[start].CompareTo(pivot) <= 0)
                        start++;
                    while (start <= end && array[end].CompareTo(pivot) >= 0)
                        end--;
                    if (end >= start)
                        SwapElement(array, start, end);
                }

                if (pivotIndex <= end)
                    if (array[pivotIndex].CompareTo(array[end]) > 0)
                        SwapElement(array, pivotIndex, end);

                if (startOfSubSet < end)
                {
                    stack.Push(startOfSubSet);
                    stack.Push(end - 1);
                }

                if (endOfSubset > end)
                {
                    stack.Push(end + 1);
                    stack.Push(endOfSubset);
                }
            }
            return array;
        }

        private static void SwapElement<T>(List<T> arr, int left, int right)
        {
            T temp = arr[left];
            arr[left] = arr[right];
            arr[right] = temp;
        }
    }
}
