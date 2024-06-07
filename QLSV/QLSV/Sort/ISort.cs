using QLSV.List;

namespace QLSV.Sort
{
    interface ISort<T>
    {
        void Swap(IMyList<T> list,int item1, int item2);
    }
}
