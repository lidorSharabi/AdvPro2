using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        private SimplePriorityQueue<State<T>> openList;
        protected int evaluatedNodes;

        public Searcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }

        protected void addToOpenList(State<T> state)
        {
            openList.Enqueue(state, 1);
        }

        protected bool openContains(State<T> state)
        {
            return openList.Contains(state);
        }

        protected State<T> popOpenList()
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

        public State<T> getOpenElement(State<T> element)
        {
            foreach (State<T> var in openList)
            {
                if (var.Equals(element))
                    return element;
            }
            return null;
        }

        public Solution<T> backTrace(ref State<T> state)
        {
            List<State<T>> vertex = new List<State<T>>();
            while (state != null)
            {
                vertex.Add(state);
                state = state.cameFrom;
            }

            Solution<T> sol = new Solution<T>(vertex);
            return sol;
        }
        public abstract Solution<T> search(ISearchable<T> searchable);
    }
}
