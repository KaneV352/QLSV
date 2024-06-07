namespace QLSV.List
{
    public abstract class Node<T>
    {
        public T data;
        public Node<T> next;

        public Node(T t)
        {
            this.data = t;
            this.next = null;
        }
    }
}