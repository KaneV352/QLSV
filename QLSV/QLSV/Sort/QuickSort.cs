using Microsoft.Office.Interop.Excel;
using QLSV.Core;
using QLSV.List;
using System;

namespace QLSV.Sort
{
    internal class QuickSort<T> : ISort<T> where T : ICmparable<T>
    {
        private ISpecification<T> _specification;

        public QuickSort(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public void Sort(IMyList<T> list, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(list, low, high);

                Sort(list, low, pi - 1);
                Sort(list, pi + 1, high);
            }
        }

        private int Partition(IMyList<T> list, int low, int high)
        {
            T pivot = list.GetIndex(high);
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (list.GetIndex(j).CompareTo(pivot, _specification) <= 0)
                {
                    i++;
                    Swap(list, i, j);
                }
            }
            Swap(list, i + 1, high);
            return i + 1;
        }


        public void Swap(IMyList<T> list, int item1, int item2)
        {
            list.Swap(item1, item2);
        }
    }
}
