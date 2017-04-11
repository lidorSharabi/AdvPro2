using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : PriorityQueSearcher<T>
    {
        public override Solution<T> search(ISearchable<T> searchable)
        {
            State<T> state = searchable.getInitializeState();
            state.cost = 1;
            state.cameFrom = null;
            addToOpenList(state , 1);
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (openListSize > 0)
            {
                State<T> n = popOpenList();
                closed.Add(n);
                if (n.Equals(searchable.getGoalState()))
                    return backTrace(ref state);
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !openContains(s))
                    {
                        s.cameFrom = n;
                        addToOpenList(s, 1);
                    }
                    else if (s.cost > n.cost + 1) // means this path is better
                    {
                        if(!openContains(s))
                        {
                            s.cameFrom = n;
                            addToOpenList(s, 1);
                        }
                        else
                        {
                            getOpenElement(s).cost = n.cost + 1; 
                        }
                    }
                }
            }
            return null;
        }

    }
}
