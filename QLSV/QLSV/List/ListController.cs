using QLSV.Core;
using QLSV.List.ArrayList;
using QLSV.List.CircularLinkedList;
using QLSV.List.DoublyLinkedList;
using QLSV.List.SinglyLinkedList;

namespace QLSV.List
{
    public class ListController<T> where T : ICmparable<T>
    {
        public ListController() { }

        public IMyList<T> ArrayList()
        {
            return new ArrayList<T>();
        }

        public IMyList<T> CircularLinkedList()
        {
            return new CircularLinkedList<T>();
        }

        public IMyList<T> DoublyLinkedList()
        {
            return new DoublyLinkedList<T>();
        }

        public IMyList<T> SinglyLinkedList()
        {
            return new SinglyLinkedList<T>();
        }
    }
}
