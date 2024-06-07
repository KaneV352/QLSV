namespace QLSV.List.DoublyLinkedList
{
    public class DoublyNode<T> : Node<T>
    {
        public DoublyNode<T> prev;

        public DoublyNode(T t) : base(t)
        {
            this.prev = null;
        }
    }
}
