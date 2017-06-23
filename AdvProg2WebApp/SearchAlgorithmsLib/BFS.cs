using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// responsible for Bfs search
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BFS<T> : PriorityQueSearcher<T>
    {
        /// <summary>
        /// search on searchable using Best First Search algorithm
        /// </summary>
        /// <param name="searchable">obj that can be searched on, has functions initialize/goal state and get all possible states from specific state</param>
        /// <param name="comparator">has function copmare which helped the comparator param to determine who's better state to came from</param>
        /// <returns>the solution of the BFS algorithm:
        /// path to the goal and the number of state that evaluated</returns>
        public override Solution<T> Search(ISearchable<T> searchable, IComparator<T> comparator)
        {
            State<T> state = searchable.GetInitializeState();
            state.Cost = 1;
            state.CameFrom = null;
            AddToOpenList(state , 1);
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> currentState = PopOpenList();
                closed.Add(currentState);
                if (currentState.Equals(searchable.GetGoalState()))
                    return BackTrace(ref currentState);

                List<State<T>> succerssors = searchable.GetAllPossibleStates(currentState);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !OpenContains(s))
                    {
                        s.Cost = currentState.Cost + 1;
                        s.CameFrom = currentState;
                        AddToOpenList(s, 1);
                    }
                    else if (comparator.Compare(s,currentState) == 1)
                    {
                        if(!OpenContains(s))
                        {
                            s.Cost = currentState.Cost + 1;
                            s.CameFrom = currentState;
                            AddToOpenList(s, 1);
                        }
                        else
                        {
                            RemoveAndAddElementToOpenList(s, currentState.Cost + 1);
                        }
                    }
                }
            }
            return null;
        }

    }
}
