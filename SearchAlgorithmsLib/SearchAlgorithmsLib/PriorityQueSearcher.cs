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

        protected void AddToOpenList(State<T> state, float priority)
        {
            openList.Enqueue(state, priority);
        }

        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        public int OpenListSize
        {
            get { return openList.Count; }
        }

        protected bool OpenContains(State<T> state)
        {
            return openList.Contains(state);
        }

        public void RemoveAndAddElementToOpenList(State<T> state, float priority)
        {
            foreach (State<T> var in openList)
            {
                if (var.Equals(state))
                {
                    openList.Remove(var);
                    openList.Enqueue(state, priority);
                    return;
                }
            }
        }
    }
}
