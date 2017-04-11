using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class PriorityQueSearcher<T> : Searcher<T>
    {
        private SimplePriorityQueue<State<T>> openList;

        public PriorityQueSearcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }

        protected void addToOpenList(State<T> state, float priority)
        {
            openList.Enqueue(state, priority);
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

        protected bool openContains(State<T> state)
        {
            return openList.Contains(state);
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
    }
}
