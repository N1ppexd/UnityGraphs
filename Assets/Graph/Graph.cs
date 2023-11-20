using System.Linq;

namespace Graph
{
    public class Graph<T>
    {
        private NodeList<T> nodeSet;

        private GraphNode<T> rootNode;
        private GraphNode<T> finalNode;


        public GraphNode<T> RootNode { get => rootNode; }
        public GraphNode<T> FinalNode { get => finalNode; }

        public Graph() : this(null) { }
        public Graph(NodeList<T> nodeSet)
        {
            if (nodeSet == null)
                this.nodeSet = new NodeList<T>();
            else
                this.nodeSet = nodeSet;
        }

        public void AddNode(GraphNode<T> node)
        {
            // adds a node to the graph
            nodeSet.Add(node);

            if (rootNode == null)
                rootNode = node;

            finalNode = node;
        }

        public void AddNode(T value)
        {
            // adds a node to the graph
            GraphNode<T> node = new GraphNode<T>(value);

            nodeSet.Add(node);

            if (rootNode == null)
                rootNode = node;

            finalNode = node;
        }

        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }
        
        public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);

            to.Neighbors.Add(from);
            to.Costs.Add(cost);
        }

        public void AddDirectedEdge(T u, T v, int cost)
        {
            GraphNode<T> from = (GraphNode<T>)nodeSet.FindByValue(u);
            GraphNode<T> to = (GraphNode<T>)nodeSet.FindByValue(v);

            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }

        public void AddUndirectedEdge(T u, T v, int cost)
        {
            GraphNode<T> from = (GraphNode<T>)nodeSet.FindByValue(u);
            GraphNode<T> to = (GraphNode<T>)nodeSet.FindByValue(v);

            from.Neighbors.Add(to);
            from.Costs.Add(cost);

            to.Neighbors.Add(from);
            to.Costs.Add(cost);
        }

        public bool Contains(T value)
        {
            return nodeSet.FindByValue(value) != null;
        }

        public bool Remove(T value)
        {
            // first remove the node from the nodeset
            GraphNode<T> nodeToRemove = (GraphNode<T>)nodeSet.FindByValue(value);
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (GraphNode<T> gnode in nodeSet.Cast<GraphNode<T>>())
            {
                int index = gnode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1)
                {
                    // remove the reference to the node and associated cost
                    gnode.Neighbors.RemoveAt(index);
                    gnode.Costs.RemoveAt(index);
                }
            }

            return true;
        }

        public NodeList<T> Nodes => nodeSet;
        public int NodeCount => nodeSet.Count;
    }
}
