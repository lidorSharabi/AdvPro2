using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : PriorityQueSearcher<T>
    {
        public override Solution<T> search(ISearchable<T> searchable, Comparator<T> comparator)
        {
            State<T> state = searchable.getInitializeState();
            state.cost = 1;
            state.cameFrom = null;
            addToOpenList(state , 1);
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (openListSize > 0)
            {
                State<T> currentState = popOpenList();
                closed.Add(currentState);
                if (currentState.Equals(searchable.getGoalState()))
                    return backTrace(ref currentState);

                List<State<T>> succerssors = searchable.getAllPossibleStates(currentState);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !openContains(s))
                    {
                        s.cost = currentState.cost + 1;
                        s.cameFrom = currentState;
                        addToOpenList(s, 1);
                    }
                    else if (comparator.compare(s,currentState) == 1)
                    {
                        if(!openContains(s))
                        {
                            s.cost = currentState.cost + 1;
                            s.cameFrom = currentState;
                            addToOpenList(s, 1);
                        }
                        else
                        {
                            getOpenElement(s).cost = currentState.cost + 1; //TODO - check if it returns by ref
                        }
                    }
                }
            }
            return null;
        }

    }
}
