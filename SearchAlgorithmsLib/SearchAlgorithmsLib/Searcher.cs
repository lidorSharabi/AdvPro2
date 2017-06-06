using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// abstract searcher class implements searcher interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// responsible to track the amounts of nodes that the algorithm calculated
        /// </summary>
        protected int evaluatedNodes;
        /// <summary>
        /// get evaluatedNodes 
        /// </summary>
        /// <returns>evaluatedNodes</returns>
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        /// <summary>
        /// calculate the path to the "state"
        /// </summary>
        /// <param name="state"></param>
        /// <returns>Solution: list with a path to the goal state,
        /// and the amounts of nodes that the algorithm calculated</returns>
        public Solution<T> BackTrace(ref State<T> state)
        {
            List<State<T>> vertex = new List<State<T>>();
            while (state != null)
            {
                vertex.Add(state);
                state = state.CameFrom;
            }

            Solution<T> sol = new Solution<T>(vertex, GetNumberOfNodesEvaluated());
            return sol;
        }
        /// <summary>
        /// execute to search according the search algoritm
        /// </summary>
        /// <param name="searchable">obj that can be searched on, has functions initialize/goal state and get all possible states from specific state</param>
        /// <param name="comparator">has function copmare which helped the comparator param to determine who's better state to came from</param>
        /// <returns></returns>
        public abstract Solution<T> Search(ISearchable<T> searchable, IComparator<T> comparator = null);
    }
}
