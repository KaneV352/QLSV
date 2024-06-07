using QLSV.Core;
using System;
using System.Collections.Generic;

namespace QLSV.List.SinglyLinkedList
{
    class SinglyLinkedList<T> : IMyList<T> where T : ICmparable<T>
    {
        private SinglyNode<T> _head;

        public Node<T> Head
        {
            get { return _head; }
        }
        private int _count = 0;

        public int Count
        {
            get { return _count; }
        }

        public SinglyLinkedList()
        {
            _head = null;
        }

        public SinglyLinkedList(T t)
        {
            _head = new SinglyNode<T>(t);
            _count = 1;
        }

        public void Add(T t)
        {
            SinglyNode<T> newNode = new SinglyNode<T>(t);
            if (_head == null)
            {
                _head = newNode;
                _head.next = null;
                _count++;
            }
            else
            {
                SinglyNode<T> last = _head;
                while (last != null && last.next != null)
                {
                    last = last.next as SinglyNode<T>;
                }
                if (last == null) return;
                last.next = newNode;
                _count++;
            }
        }

        public void Remove(T t, ISpecification<T> specification)
        {
            if (_head == null) return;

            SinglyNode<T> temp = _head;
            SinglyNode<T> prev = null;


            if (temp != null && temp.data.CompareTo(t, specification) == 0)
            {
                _head = temp.next as SinglyNode<T>;
                _count--;
                return;
            }

            while (temp != null && temp.data != null && temp.data.CompareTo(t, specification) != 0)
            {
                prev = temp;
                temp = temp.next as SinglyNode<T>;
            }

            if (temp == null)
            {
                return;
            }

            if (prev != null && temp.next != null)
                prev.next = temp.next;
            _count--;
        }

        public T GetIndex(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Index is out of range");

            SinglyNode<T> temp = _head;
            for (int i = 0; i < index; i++)
            {
                if (temp == null)
                    throw new ArgumentNullException("temp");

                temp = temp.next as SinglyNode<T>;
            }

            if (temp == null)
                throw new ArgumentNullException("temp");

            return temp.data;
        }

        public void SetIndex(int index, T t)
        {
            SinglyNode<T> temp = _head;

            if (temp == null)
            {
                return;
            }

            for (int i = 0; i < index; i++)
            {
                temp = temp.next as SinglyNode<T>;
            }

            temp.data = t;
        }

        public bool IsContains(T t)
        {
            SinglyNode<T> temp = _head;

            while (temp != null)
            {
                if (temp.data != null && temp.data.Equals(t))
                {
                    return true;
                }
                temp = temp.next as SinglyNode<T>;
            }

            return false;
        }

        public IEnumerable<T> Find(T t, ISpecification<T> specification, Action<IMyList<T>, ISpecification<T>> sortFuction)
        {
            sortFuction(this, specification);

            for (var temp = _head; temp != null; temp = temp.next as SinglyNode<T>)
            {
                if (temp.data != null && ((ICmparable<T>)temp.data).CompareTo(t, specification) == 0)
                {
                    yield return temp.data;

                    if (temp.next == null || temp.next.data.CompareTo(t, specification) != 0) break;
                }

            }
        }

        public IEnumerable<T> PrintList()
        {
            for (var temp = _head; temp != null; temp = temp.next as SinglyNode<T>)
            {
                yield return temp.data;
            }
        }

        public void Swap(int index1, int index2)
        {
            SinglyNode<T> node1 = _head;
            for (int i = 0; i < index1; i++)
            {
                if (node1 == null)
                    throw new ArgumentNullException("node1");
                node1 = node1.next as SinglyNode<T>;
            }
            SinglyNode<T> node2 = _head;
            for (int i = 0; i < index2; i++)
            {
                if (node2 == null)
                    throw new ArgumentNullException("node2");
                node2 = node2.next as SinglyNode<T>;
            }

            if (node1 == null)
                throw new ArgumentNullException("node1");

            if (node2 == null)
                throw new ArgumentNullException("node2");

            T temp = node1.data;
            node1.data = node2.data;
            node2.data = temp;
        }
    }
}
