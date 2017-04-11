using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : Searcher<T>
    {
        public override Solution<T> search(ISearchable<T> searchable)
        {
            addToOpenList(searchable.getInitializeState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (openListSize > 0)
            {
                State<T> n = popOpenList();
                closed.Add(n);
                if (n.Equals(searchable.getGoalState()))
                    return new Solution<T>(n);
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !openContains(s))
                    {
                        s.cameFrom = n;
                        addToOpenList(s);
                    }
                    else if (s.cost > n.cost + 1) // means this path is better
                    {
                        if(!openContains(s))
                        {
                            s.cameFrom = n;
                            addToOpenList(s);
                        }
                        else
                        {
                            getOpenElement(s).cost = n.cost + 1; 
                        }
                    }
                }
            }
            return new Solution<T>(null);
        }

    }
}
