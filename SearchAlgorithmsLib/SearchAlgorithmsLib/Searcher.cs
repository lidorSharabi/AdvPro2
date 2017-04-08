using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher : ISearcher
    {
        private SimplePriorityQueue<State> openList;
        private int evaluatedNodes;

        public Searcher()
        {
            openList = new SimplePriorityQueue<State>();
            evaluatedNodes = 0;
        }

        protected void addToOpenList(State state)
        {
            openList.Enqueue(state, 1);
        }

        protected State popOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        public int openListSize
        {
            get { return openList.Count; }
        }

        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        public abstract Solution search(ISearchable searchable);
    }
}
