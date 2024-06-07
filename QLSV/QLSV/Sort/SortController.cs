using QLSV.Core;
using QLSV.List;

namespace QLSV.Sort
{
    public class SortController<T> where T : ICmparable<T>
    {
        public SortController()
        {

        }

        public void BubbleSort(IMyList<T> list,ISpecification<T> specification) 
        {
            new BubbleSort<T>(specification).Sort(list);
        }

        public void HeapSort(IMyList<T> list, ISpecification<T> specification)
        {
            new HeapSort<T>(specification).Sort(list);
        }

        public void InsertionSort(IMyList<T> list, ISpecification<T> specification)
        {
            new InsertionSort<T>(specification).Sort(list);
        }

        public void MergeSort(IMyList<T> list, ISpecification<T> specification)
        {
            new MergeSort<T>(specification).Sort(list, 0, list.Count - 1);
        }

        public void QuickSort(IMyList<T> list, ISpecification<T> specification)
        {
            new QuickSort<T>(specification).Sort(list, 0, list.Count - 1);
        }

        public void SelectionSort(IMyList<T> list, ISpecification<T> specification)
        {
            new SelectionSort<T>(specification).Sort(list);
        }
    }
}
