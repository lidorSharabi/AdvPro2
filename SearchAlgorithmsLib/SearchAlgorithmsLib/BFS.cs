using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : PriorityQueSearcher<T>
    {
        public override Solution<T> Search(ISearchable<T> searchable, Comparator<T> comparator)
        {
            State<T> state = searchable.GetInitializeState();
            state.cost = 1;
            state.cameFrom = null;
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
                        s.cost = currentState.cost + 1;
                        s.cameFrom = currentState;
                        AddToOpenList(s, 1);
                    }
                    else if (comparator.Compare(s,currentState) == 1)
                    {
                        if(!OpenContains(s))
                        {
                            s.cost = currentState.cost + 1;
                            s.cameFrom = currentState;
                            AddToOpenList(s, 1);
                        }
                        else
                        {
                            RemoveAndAddElementToOpenList(s, currentState.cost + 1); //TODO - check if it returns by ref
                        }
                    }
                }
            }
            return null;
        }

    }
}
