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

        protected void AddToOpenList(State<T> state)
        {
            openList.Push(state);
        }

        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            return openList.Pop();
        }

        public int OpenListSize
        {
            get { return openList.Count; }
        }

    }
}
