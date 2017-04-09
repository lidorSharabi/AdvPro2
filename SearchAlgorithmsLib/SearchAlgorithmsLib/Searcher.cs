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
        public Solution backTrace()
        {

        }

        public abstract Solution search(ISearchable<T> searchable);
    }
}
