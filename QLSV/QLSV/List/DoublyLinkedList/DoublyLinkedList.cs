using System;
using System.Collections.Generic;
using QLSV.Core;
using QLSV.List.SinglyLinkedList;

namespace QLSV.List.DoublyLinkedList
{
        public class DoublyLinkedList<T> : IMyList<T> where T : ICmparable<T>
        {
            private DoublyNode<T> _head;
            private int _count = 0;

            public int Count => _count;

            public Node<T> Head => _head;

            public DoublyLinkedList()
            {
                _head = null;
            }

            public DoublyLinkedList(T t)
            {
                _head = new DoublyNode<T>(t);
                _count = 1;
            }

            public void Add(T t)
            {
                DoublyNode<T> newNode = new DoublyNode<T>(t);

                if (_head == null)
                {
                    _head = newNode;
                    _count = 1;
                    return;
                }

                DoublyNode<T> last = _head;
                while (last != null && last.next != null)
                {
                    last = last.next as DoublyNode<T>;
                }

                if (last == null) return;
                last.next = newNode;
                newNode.prev = last;
                _count++;
            }

            public void Remove(T t, ISpecification<T> specification)
            {
                if (_head == null) return;

                if (_head.data != null && _head.data.CompareTo(t, specification) == 0)
                {
                    _head = _head.next as DoublyNode<T>;
                    if (_head != null)
                    {
                        _head.prev = null;
                    _count--;
                    }
                    return;
                }

                DoublyNode<T> temp = _head;
                while (temp != null && temp.data != null && temp.data.CompareTo(t, specification) != 0)
                {
                    temp = temp.next as DoublyNode<T>;
                }

                if (temp == null) return;
                temp.prev.next = temp.next;
                if (temp.next != null)
                {
                    DoublyNode<T> nextNode = temp.next as DoublyNode<T>;
                    if (nextNode != null)
                        nextNode.prev = temp.prev;
                    _count--;
                }
            }

            public T GetIndex(int index)
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException("Index out of range");

                DoublyNode<T> temp = _head;
                for (int i = 0; i < index; i++)
                {
                    if (temp == null)
                        throw new ArgumentNullException("temp");
                    temp = temp.next as DoublyNode<T>;
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

            DoublyNode<T> temp = _head;

            if (temp == null)
            {
                return;
            }

            for (int i = 0; i < index; i++)
            {
                temp = temp.next as DoublyNode<T>;
            }

            temp.data = t;
        }

        public bool IsContains(T t)
            {
                DoublyNode<T> temp = _head;

                while (temp != null)
                {
                    if (temp.data != null && temp.data.Equals(t))
                    {
                        return true;
                    }
                    temp = temp.next as DoublyNode<T>;
                }

                return false;
            }

            public IEnumerable<T> Find(T t, ISpecification<T> specification, Action<IMyList<T>, ISpecification<T>> sortFuction)
            {
                sortFuction(this, specification);

                DoublyNode<T> temp = _head;

                while (temp != null)
                {
                    if (temp.data != null && temp.data.CompareTo(t, specification) == 0)
                    {
                        yield return temp.data;

                        if (temp.next == null || temp.next.data.CompareTo(t, specification) != 0) break;
                    }
                    temp = temp.next as DoublyNode<T>;
                }
            }

        public IEnumerable<T> PrintList()
        {
            for (var temp = _head; temp != null; temp = temp.next as DoublyNode<T>)
            {
                yield return temp.data;
            }
        }

        public void Swap(int index1, int index2)
            {
                DoublyNode<T> node1 = _head;
                for (int i = 0; i < index1; i++)
                {
                    if (node1 == null)
                        throw new ArgumentNullException("node1");
                    node1 = node1.next as DoublyNode<T>;
                }
                DoublyNode<T> node2 = _head;
                for (int i = 0; i < index2; i++)
                {
                    if (node2 == null)
                        throw new ArgumentNullException("node2");
                    node2 = node2.next as DoublyNode<T>;
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
