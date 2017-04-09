using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
     public abstract class Searcher<T> : ISearcher<T>
    {
        protected int evaluatedNodes;

        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        public Solution<T> backTrace()
        {
            return null;
        }

        public abstract Solution<T> search(ISearchable<T> searchable);
    }
}
