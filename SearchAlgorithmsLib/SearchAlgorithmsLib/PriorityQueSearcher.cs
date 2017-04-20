using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class PriorityQueSearcher<T> : Searcher<T>
    {
        /// <summary>
        /// a list of all the states that need to evaluate
        /// </summary>
        private SimplePriorityQueue<State<T>> openList;
        /// <summary>
        /// initialized the Priority list and the amount of nodes that evaluated to 0
        /// </summary>
        public PriorityQueSearcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }
        /// <summary>
        /// add state to the open list with priority
        /// </summary>
        /// <param name="state">which state to add</param>
        /// <param name="priority">with priority to be evaluated</param>
        protected void AddToOpenList(State<T> state, float priority)
        {
            openList.Enqueue(state, priority);
        }
        /// <summary>
        /// add 1 to the amount of nodes that evaluated and remove the best node
        /// </summary>
        /// <returns>the state with the highest priority</returns>
        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }
        /// <summary>
        /// get openList
        /// </summary>
        public int OpenListSize
        {
            get { return openList.Count; }
        }
        /// <summary>
        /// check if openList contains "state"
        /// </summary>
        /// <param name="state"></param>
        /// <returns>return openList.Contains(state)</returns>
        protected bool OpenContains(State<T> state)
        {
            return openList.Contains(state);
        }
        /// <summary>
        /// remove "state" from open list and add it again with new priority
        /// </summary>
        /// <param name="state">the "state" that we change his priority</param>
        /// <param name="priority">the new priority of "state"</param>
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
