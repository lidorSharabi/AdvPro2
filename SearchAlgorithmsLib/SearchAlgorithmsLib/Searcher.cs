using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        protected int evaluatedNodes;

        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        public Solution<T> backTrace(ref State<T> state)
        {
            List<State<T>> vertex = new List<State<T>>();
            while (state != null)
            {
                vertex.Add(state);
                state = state.cameFrom;
            }

            Solution<T> sol = new Solution<T>(vertex, getNumberOfNodesEvaluated());
            return sol;
        }

        public abstract Solution<T> search(ISearchable<T> searchable, Comparator<T> comparator = null);
    }
}
