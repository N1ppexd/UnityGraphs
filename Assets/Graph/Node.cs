

namespace Graph
{
    public class Node<T>
    {

        public T Value { get; set; }

        protected NodeList<T> Neighbors { get; set; } = null;

        public Node() { }
        public Node(T data) : this(data, null) { }
        public Node(T data, NodeList<T> neighbors)
        {
            Value = data;
            Neighbors = neighbors;
        }
    }
}

