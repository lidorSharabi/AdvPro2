using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MazeLib;

namespace SearchAlgorithmsLib
{

    public class DFS<T> : StackSearcher<T>
    {
        /// <summary>
        /// search on searchable using DFS algorithm
        /// </summary>
        /// <param name="searchable">obj that can be searched on</param>
        /// <param name="comparator">irrelevant</param>
        /// <returns>the solution of the DFS algorithm:
        /// path to the goal and the number of state that evaluated</returns>
        public override Solution<T> Search(ISearchable<T> searchable, IComparator<T> comparator = null)
        {
            HashSet<T> labeled = new HashSet<T>();
            State<T> state = searchable.GetInitializeState();
            state.Cost = 1;
            state.CameFrom = null;
            AddToOpenList(state);
            while (OpenListSize > 0)
            {
                state = PopOpenList();
                if (state.Equals(searchable.GetGoalState()))
                {
                    return BackTrace(ref state);
                }
                if (!labeled.Contains(state.Getstate()))
                {
                    labeled.Add(state.Getstate());
                    List<State<T>> succerssors = searchable.GetAllPossibleStates(state);
                    foreach (State<T> s in succerssors)
                    {
                        if (!labeled.Contains(s.Getstate()))
                        {
                            s.Cost = state.Cost + 1;
                            s.CameFrom = state;
                            AddToOpenList(s);
                        }
                    }

                }
            }
            return null;
        }
    }
}
