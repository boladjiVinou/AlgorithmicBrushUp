using skiena.datastructures.graph;
using skiena.datastructures.trees;
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

        /*
         5.7
        We can reconstruct tree with inorder + (post or pre order)
        if the values are distinct
        We cannot reconstruct an unique tree with only pre and post order
        A
         \
          B
         /
        C

        A
       /
       B
        \
         C
        both have as pre order : ABC and post order CBA but they are different
         */
        public static MyBST<int> reconstructTreeWithPreOrderAndInOrder(List<int> preOrderVisit, List<int> inOrderVisit) 
        {
            return MyBST<int>.buildFromPreOrderVisitAndInOrderVisit(preOrderVisit, inOrderVisit);
        }
        public static MyBST<int> reconstructTreeWithPostOrderAndInOrder(List<int> postOrderVisit, List<int> inOrderVisit)
        {
            return MyBST<int>.buildFromPostOrderVisitAndInOrderVisit(postOrderVisit, inOrderVisit);
        }
    }
}
