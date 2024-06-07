using QLSV.Core;
using System;
using System.Collections.Generic;

namespace QLSV.List
{
    public interface IMyList<T>
    {
        void Add(T t);
        void Remove(T t, ISpecification<T> specification);
        T GetIndex(int index);
        void SetIndex(int index, T t);
        IEnumerable<T> Find(T t, ISpecification<T> specification, Action<IMyList<T>, ISpecification<T>> sortFuction);
        bool IsContains(T t);
        void Swap(int i, int j);
        int Count { get; }
        IEnumerable<T> PrintList();
    }
}