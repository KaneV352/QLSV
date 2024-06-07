using QLSV.Core;
using System.Collections.Generic;
using System;
using QLSV.List.SinglyLinkedList;

namespace QLSV.List.CircularLinkedList
{
    public class CircularLinkedList<T> : IMyList<T> where T : ICmparable<T>
    {
        private CircularNode<T> _head;
        private int _count = 0;

        public int Count => _count;

        public Node<T> Head => _head;

        public CircularLinkedList()
        {
            _head = null;
        }

        public CircularLinkedList(T t)
        {
            _head = new CircularNode<T>(t);
            _head.next = _head;
            _count = 1;
        }

        public void Add(T t)
        {
            CircularNode<T> newNode = new CircularNode<T>(t);
            if (_head == null)
            {
                _head = newNode;
                _head.next = _head;
                _count = 1;
            }
            else
            {
                CircularNode<T> last = _head;
                while (last != null && last.next != _head)
                {
                    last = last.next as CircularNode<T>;
                }
                if (last == null) return;
                last.next = newNode;
                newNode.next = _head;
                _count++;
            }
        }


        public void Remove(T t, ISpecification<T> specification)
        {
            if (_head == null) return;

            CircularNode<T> temp = _head;
            CircularNode<T> prev = null;

            if (temp != null && temp.data != null && temp.data.CompareTo(t, specification) == 0)
            {
                _head = temp.next as CircularNode<T>;
                _count--;
                return;
            }

            while (temp != null && temp.data != null && temp.data.CompareTo(t, specification) != 0)
            {
                prev = temp;
                temp = temp.next as CircularNode<T>;
            }

            if (temp == null || prev == null) return;
            prev.next = temp.next;
            _count--;
        }

        public T GetIndex(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Index out of range");

            CircularNode<T> temp = _head;
            for (int i = 0; i < index; i++)
            {
                if (temp == null)
                    throw new ArgumentNullException("temp");
                temp = temp.next as CircularNode<T>;
            }

            if (temp == null)
                throw new ArgumentNullException("temp");
            return temp.data;
        }

        public void SetIndex(int index, T t)
        {
            if (index >= _count)
            {
                Console.Error.WriteLine(new IndexOutOfRangeException().Message);
                return;
            }

            CircularNode<T> temp = _head;

            if (temp == null)
            {
                return;
            }

            for (int i = 0; i < index; i++)
            {
                temp = temp.next as CircularNode<T>;
            }

            temp.data = t;
        }

        public bool IsContains(T t)
        {
            CircularNode<T> temp = _head;
            while (temp != null)
            {
                if (temp.data != null && temp.data.Equals(t))
                {
                    return true;
                }
                temp = temp.next as CircularNode<T>;
                if (temp == _head) return false;
            }
            return false;
        }

        public IEnumerable<T> Find(T t, ISpecification<T> specification, Action<IMyList<T>, ISpecification<T>> sortFuction)
        {
            sortFuction(this, specification);

            CircularNode<T> temp = _head;

            do
            {
                if (temp.data != null && temp.data.CompareTo(t, specification) == 0)
                {
                    yield return temp.data;

                    if (temp.next != _head || temp.next.data.CompareTo(t, specification) != 0) break;
                }
                temp = temp.next as CircularNode<T>;
            }
            while (temp != _head);
        }
        public IEnumerable<T> PrintList()
        {
            for (var temp = _head; temp != null; temp = temp.next as CircularNode<T>)
            {
                yield return temp.data;
            }
        }

        public void Swap(int index1, int index2)
        {
            CircularNode<T> node1 = _head;
            for (int i = 0; i < index1; i++)
            {
                if (node1 == null)
                    throw new ArgumentNullException("node1");
                node1 = node1.next as CircularNode<T>;
            }
            CircularNode<T> node2 = _head;
            for (int i = 0; i < index2; i++)
            {
                if (node2 == null)
                    throw new ArgumentNullException("node2");
                node2 = node2.next as CircularNode<T>;
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