using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public abstract class StackSearcher<T> : Searcher<T>
    {
        private Stack<State<T>> openList;

        public StackSearcher()
        {
            openList = new Stack<State<T>>();
            evaluatedNodes = 0;
        }

        protected void addToOpenList(State<T> state)
        {
            openList.Push(state);
        }

        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return openList.Pop();
        }

        public int openListSize
        {
            get { return openList.Count; }
        }

    }
}
