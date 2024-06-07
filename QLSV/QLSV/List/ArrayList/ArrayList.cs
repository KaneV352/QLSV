using QLSV.Core;
using System.Collections.Generic;
using System.Linq;
using QLSV.Sort;
using System;

namespace QLSV.List.ArrayList
{
    public class ArrayList<T> : IMyList<T> where T : ICmparable<T>
    {
        private T[] _list;
        private bool _isSorted = false;


        public ArrayList()
        {
            _list = new T[0];
        } 

        public int Count => _list.Length;

        public void Add(T t)
        {
            List<T> list = _list.ToList();
            list.Add(t);
            _list = list.ToArray();
        }

        public IEnumerable<T> Find(T t, ISpecification<T> specification, Action<IMyList<T>, ISpecification<T>> sortFuction)
        {
            //for (int i = 0; i < Count; i++)
            //{
            //    if (specification == null) yield break;

            //    if (_list[i].CompareTo(t, specification) == 0)
            //    {
            //        yield return _list[i];
            //    }
            //}

            // _sortController.QuickSort(this, specification);

            sortFuction(this, specification);

            var resultList = BinarySearch(t, specification, 0, Count - 1);

            foreach (var item in resultList)
            {
                yield return item;
            }
        }

        public IEnumerable<T> BinarySearch(T t, ISpecification<T> specification, int low, int high)
        {
            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                int cmp = _list[mid].CompareTo(t, specification);

                if (cmp < 0)
                {
                    low = mid + 1;
                }
                else if (cmp > 0)
                {
                    high = mid - 1;
                }
                else
                {
                    // Found a match. Now find the first occurrence.
                    while (mid > low && _list[mid - 1].CompareTo(t, specification) == 0)
                    {
                        mid--;
                    }

                    // Yield all equal elements.
                    while (mid <= high && _list[mid].CompareTo(t, specification) == 0)
                    {
                        yield return _list[mid];
                        mid++;
                    }

                    break;
                }
            }
        }


        public T GetIndex(int index)
        {
            return _list[index];
        }

        public bool IsContains(T t)
        {
            return _list.Contains(t);
        }

        public void Remove(T t, ISpecification<T> specification)
        {
            int index = -1;
            for (int i = 0; i < _list.Length; i++)
            {
                if (_list[i].CompareTo(t, specification) == 0)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                T[] newList = new T[_list.Length - 1];
                Array.Copy(_list, 0, newList, 0, index);
                Array.Copy(_list, index + 1, newList, index, _list.Length - index - 1);
                _list = newList;
            }
        }



        public void SetIndex(int index, T t)
        {
            _list[index] = t;
        }

        public IEnumerable<T> PrintList()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return _list[i];
            }
        }

        public void Swap(int i, int j)
        {
            T temp = _list[i];
            _list[i] = _list[j];
            _list[j] = temp;
        }
    }
}
