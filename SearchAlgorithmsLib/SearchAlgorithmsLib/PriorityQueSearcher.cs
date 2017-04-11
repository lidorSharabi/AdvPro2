using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class PriorityQueSearcher<T> : Searcher<T>
    {
        private SimplePriorityQueue<T> openList;

        public PriorityQueSearcher()
        {
            openList = new SimplePriorityQueue<T>();
            evaluatedNodes = 0;
        }

        protected void addToOpenList(T state, float priority)
        {
            openList.Enqueue(state, priority);
        }

        protected T popOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        public int openListSize
        {
            get { return openList.Count; }
        }

    }
}
