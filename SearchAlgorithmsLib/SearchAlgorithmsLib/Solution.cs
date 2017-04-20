using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        /// <summary>
        /// list that represent the solution path
        /// </summary>
        public List<State<T>> vertex { get; }
        /// <summary>
        /// responsible to track the amounts of nodes that the algorithm calculated
        /// </summary>
        public int evaluatedNodes { get; }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="list">the solution path</param>
        /// <param name="evalNodes">amounts of nodes that the algorithm calculated</param>
        public Solution(List<State<T>> list, int evalNodes)
        {
            vertex = list;
            evaluatedNodes = evalNodes;
        }
    }
}
