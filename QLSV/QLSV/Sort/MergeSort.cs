using QLSV.Core;
using QLSV.List;
using System;

namespace QLSV.Sort
{
    public class MergeSort<T> : ISort<T> where T : ICmparable<T>
    {
        private ISpecification<T> _specification;

        public MergeSort(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public void Sort(IMyList<T> list, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;

                Sort(list, left, mid);
                Sort(list, mid + 1, right);

                Merge(list, left, mid, right);
            }
        }

        private void Merge(IMyList<T> list, int left, int mid, int right)
        {
            T[] temp = new T[right - left + 1];

            int i = left;
            int j = mid + 1;
            int k = 0;

            while (i <= mid && j <= right)
            {
                if (list.GetIndex(i).CompareTo(list.GetIndex(j), _specification) <= 0)
                {
                    temp[k] = list.GetIndex(i);
                    i++;
                }
                else
                {
                    temp[k] = list.GetIndex(j);
                    j++;
                }
                k++;
            }

            while (i <= mid)
            {
                temp[k] = list.GetIndex(i);
                i++;
                k++;
            }

            while (j <= right)
            {
                temp[k] = list.GetIndex(j);
                j++;
                k++;
            }

            for (i = left; i <= right; i++)
            {
                list.SetIndex(i, temp[i - left]);
            }
        }


        public void Swap(IMyList<T> list, int item1, int item2)
        {
            list.Swap(item1, item2);
        }
    }

}
