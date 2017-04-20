using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public abstract class StackSearcher<T> : Searcher<T>
    {
        /// <summary>
        ///  a list of all the states that need to evaluate
        /// </summary>
        private Stack<State<T>> openList;
        /// <summary>
        /// initialized the open list and the amount of nodes that evaluated to 0
        /// </summary>
        public StackSearcher()
        {
            openList = new Stack<State<T>>();
            evaluatedNodes = 0;
        }
        /// <summary>
        /// add "state" to open list
        /// </summary>
        /// <param name="state"></param>
        protected void AddToOpenList(State<T> state)
        {
            openList.Push(state);
        }
        /// <summary>
        /// remove and return the the top state of open list
        /// </summary>
        /// <returns>eturn the the top state of open list</returns>
        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            return openList.Pop();
        }
        /// <summary>
        /// return the size of open list
        /// </summary>
        public int OpenListSize
        {
            get { return openList.Count; }
        }

    }
}
