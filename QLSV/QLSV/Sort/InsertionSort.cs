using Microsoft.Office.Interop.Excel;
using QLSV.Core;
using QLSV.List;
using System;

namespace QLSV.Sort
{
    internal class InsertionSort<T> : ISort<T> where T : ICmparable<T>
    {
        private ISpecification<T> _specification;

        public InsertionSort(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public void Sort(IMyList<T> list)
        {
            int n = list.Count;
            for (int i = 1; i < n; ++i)
            {
                T key = list.GetIndex(i);
                int j = i - 1;

                // Move elements of arr[0..i-1], that are greater than key, to one position ahead of their current position
                while (j >= 0 && list.GetIndex(j).CompareTo(key, _specification) > 0)
                {
                    list.SetIndex(j + 1, list.GetIndex(j));
                    j = j - 1;
                }
                list.SetIndex(j + 1, key);
            }
        }

        public void Swap(IMyList<T> list, int item1, int item2)
        {
            list.Swap(item1, item2);
        }
    }

}