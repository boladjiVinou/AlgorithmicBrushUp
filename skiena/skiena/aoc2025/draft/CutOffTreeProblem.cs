using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.aoc2025.draft
{
    public class CutOffTreeProblem
    {

        private enum Direction
        {
            none, right, bottom, left, top
        }

        private record Step(Tuple<int, int> pos, int nbSteps, Direction dir, int targetIdx,Step? prev)
        {

        }
        private record VisitState(Tuple<int, int> pos)
        {

        }
        public static int CutOffTree(IList<IList<int>> forest)
        {
            List<Tuple<int, int>> trees = [];
            for (int i = 0; i < forest.Count; i++)
            {
                for (int j = 0; j < forest[i].Count; j++)
                {
                    if (forest[i][j] < 1)
                    {
                        continue;
                    }
                    Tuple<int, int> index = new Tuple<int, int>(i, j);
                    trees.Add(index);
                }
            }
            trees.Sort((x, y) => forest[y.Item1][y.Item2].CompareTo(forest[x.Item1][x.Item2]));

            Queue<Step> bfsQueue = new Queue<Step>();
            bfsQueue.Enqueue(new Step(new Tuple<int, int>(0, 0), 0, Direction.none, trees.Count - 1, null));

            HashSet<VisitState> visited = [];
            Step? lastStep = null;
            int nbSteps = int.MaxValue;

            while (bfsQueue.Count > 0)
            {
                var currStep = bfsQueue.Dequeue();
                var curr = currStep.pos;
                var visitState = new VisitState(currStep.pos);
                if (visited.Contains(visitState))
                {
                    continue;
                }
                visited.Add(visitState);


                var nbStep = currStep.nbSteps + 1;

                int targetIdx = currStep.targetIdx;
                var targetTree = trees[targetIdx];
                if (curr.Item1 == targetTree.Item1 && curr.Item2 == targetTree.Item2)
                {
                    --targetIdx;
                    lastStep = currStep;
                    visited = [];
                    bfsQueue = [];
                }
                if (targetIdx < 0)
                {
                    nbSteps = Math.Min(nbSteps, currStep.nbSteps);
                    break;
                }
                targetTree = trees[targetIdx];

                if (curr.Item1 < forest.Count - 1 && isVisitable(forest, curr.Item1 + 1, curr.Item2))
                {
                    bfsQueue.Enqueue(new Step(new Tuple<int, int>(curr.Item1 + 1, curr.Item2), nbStep, Direction.bottom,targetIdx, null));
                }
                if (curr.Item2 < forest[0].Count - 1 && isVisitable(forest, curr.Item1, curr.Item2 + 1))
                {
                    bfsQueue.Enqueue(new Step(new Tuple<int, int>(curr.Item1, curr.Item2 + 1), nbStep, Direction.right, targetIdx, null));
                }
                if (curr.Item1 > 0 && isVisitable(forest, curr.Item1 - 1, curr.Item2))
                {
                    bfsQueue.Enqueue(new Step(new Tuple<int, int>(curr.Item1 - 1, curr.Item2), nbStep, Direction.top, targetIdx, null));
                }
                if (curr.Item2 > 0 && isVisitable(forest, curr.Item1, curr.Item2 - 1))
                {
                    bfsQueue.Enqueue(new Step(new Tuple<int, int>(curr.Item1, curr.Item2 - 1), nbStep, Direction.left, targetIdx, null));
                }

            }
            return nbSteps == int.MaxValue ? -1 : nbSteps;

        }

        private static bool isVisitable(IList<IList<int>> forest, int i, int j)
        {
            return i >= 0 && j >= 0 && i < forest.Count && j < forest[0].Count && forest[i][j]>0;
        }
    }
}
