using QLSV.Core;
using QLSV.List;
using System;

namespace QLSV.Sort
{
    internal class BubbleSort<T> : ISort<T> where T : ICmparable<T>
    {
        private ISpecification <T> _specification;

        public BubbleSort(ISpecification<T> specification) 
        {
            _specification = specification;
        }

        public void Sort(IMyList<T> list)
        {
            int n = list.Count;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (list.GetIndex(j).CompareTo(list.GetIndex(j + 1), _specification) > 0)
                    {
                        swapped = true;
                        Swap(list, i, j);
                    }
                }

                // If no two elements were swapped in inner loop, then the array is sorted.
                if (!swapped)
                    break;
            }
        }


        public void Swap(IMyList<T> list, int item1, int item2)
        {
            list.Swap(item1, item2);
        }
    }
}
