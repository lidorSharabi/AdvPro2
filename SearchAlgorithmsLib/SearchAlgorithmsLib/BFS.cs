using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class BFS : Searcher
    {
        public override Solution search(ISearchable searchable)
        {
            addToOpenList(searchable.getInitializeState());
            HashSet<State> closed = new HashSet<State>();
            while (openListSize > 0)
            {
                State n = popOpenList();
                closed.Add(n);
                if (n.Equals(searchable.getGoalState()))
                    return backTrace();
                List<State> succerssors = searchable.getAllPossibleStates(n);
                foreach (State s in succerssors)
                {
                    if (!closed.Contains(s) && !openContains(s))
                    {
                        addToOpenList(s);
                    }
                    else
                    {
                    }
                }
            }
        }

    }
}
