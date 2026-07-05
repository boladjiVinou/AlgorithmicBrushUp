using skiena.datastructures.graph.specificgraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class GraphUtil<T> where T : IEquatable<T>, IComparable<T>
    {
        static UndirectedGraph<T> primMinimumSpanningTree(UndirectedGraph<T> graph, T startingVertex) 
        {
            UndirectedGraph<T> mst = new UndirectedGraph<T>();
           // PriorityQueue<T,>
            return mst;
        }
       /* static UndirectedGraph<T> kruskalMinimumSpanningTree(UndirectedGraph<T> graph)
        {
        }*/
    }
}
