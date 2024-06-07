using QLSV.Core;
using QLSV.List;
using System;

namespace QLSV.Sort
{
    internal class HeapSort<T> : ISort<T> where T : ICmparable<T>
    {
        private ISpecification<T> _specification;

        public HeapSort(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public void Sort(IMyList<T> list)
        {
            int n = list.Count;

            // Build heap
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(list, n, i);

            // One by one extract an element from heap
            for (int i = n - 1; i >= 0; i--)
            {
                // Move current root to end
                T temp = list.GetIndex(0);
                list.SetIndex(0, list.GetIndex(i));
                list.SetIndex(i, temp);

                // call max heapify on the reduced heap
                Heapify(list, i, 0);
            }
        }

        public void Swap(IMyList<T> list, int item1, int item2)
        {
            list.Swap(item1, item2);
        }

        private void Heapify(IMyList<T> list, int n, int i)
        {
            int largest = i; // Initialize largest as root
            int left = 2 * i + 1; // left = 2*i + 1
            int right = 2 * i + 2; // right = 2*i + 2

            T largestItem = list.GetIndex(largest);

            // If left child is larger than root
            if (left < n)
            {
                T leftItem = list.GetIndex(left);
                if (leftItem.CompareTo(largestItem, _specification) > 0)
                {
                    largest = left;
                    largestItem = leftItem;
                }
            }

            // If right child is larger than largest so far
            if (right < n)
            {
                T rightItem = list.GetIndex(right);
                if (rightItem.CompareTo(largestItem, _specification) > 0)
                {
                    largest = right;
                    largestItem = rightItem;
                }
            }

            // If largest is not root
            if (largest != i)
            {
                // Swap
                T swap = list.GetIndex(i);
                list.SetIndex(i, largestItem);
                list.SetIndex(largest, swap);

                // Recursively heapify the affected sub-tree
                Heapify(list, n, largest);
            }
        }

    }

}
