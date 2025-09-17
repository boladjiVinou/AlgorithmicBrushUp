using skiena.datastructures.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter5
{
    public class Chapter5
    {
        /*
         5-5: No the graph is not necessarly bipartite ex: triangle
         */
        public static int computeChromaticNumber(Graph<int> graph) 
        {
            return graph.computeChromaticNumberGreedy();
        }
        /*
         5-6
        a) v being the root of a tree of n-1 other nodes, the other nodes being leaves
        b) v being the root of a linked list of n-1 nodes
        c) v being the root of a binary tree, having done the dfs of the left sub tree of v
         */
    }
}
