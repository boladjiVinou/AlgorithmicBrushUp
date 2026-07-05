namespace skiena.datastructures.graph.interfaces
{
    public interface IGraph<T> where T : IEquatable<T>
    {
        Dictionary<T, HashSet<T>> getAdjencyList();
        List<Tuple<T, T>> getEdges();
        List<T> getVertices();
        Dictionary<T, Dictionary<T, bool>> getAdjencyMatrice();
        void insertNode(T val);
        HashSet<T> getNeighbors(T n);
        bool isBipartite(out Dictionary<T, int> colorByNode);
        bool areNodeConnected(T n1, T n2);
        List<GraphNode<T>> getDeletionOrder();
    }
}
