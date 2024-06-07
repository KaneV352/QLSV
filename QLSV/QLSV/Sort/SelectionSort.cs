using QLSV.Core;
using QLSV.List;
using System;

namespace QLSV.Sort
{
    internal class SelectionSort<T> : ISort<T> where T : ICmparable<T>
    {
        private ISpecification<T> _specification;

        public SelectionSort(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public void Sort(IMyList<T> list)
        {
            int n = list.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (list.GetIndex(j).CompareTo(list.GetIndex(minIndex), _specification) < 0)
                    {
                        minIndex = j;
                    }
                }

                // Swap the found minimum element with the first element
                T temp = list.GetIndex(minIndex);
                list.SetIndex(minIndex, list.GetIndex(i));
                list.SetIndex(i, temp);
            }
        }

        public void Swap(IMyList<T> list, int item1, int item2)
        {
            list.Swap(item1, item2);
        }
    }

}
