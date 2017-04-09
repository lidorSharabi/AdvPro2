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
            //addToOpenList(searchable.getInitializeState());
            //HashSet<State> closed = new HashSet<State>();
            //while (openListSize > 0)
            //{
            //    State n = popOpenList();
            //    closed.Add(n);
            //    if (n.Equals(searchable.getGoalState()))
            //        return new Solution(n);
            //    List<State> succerssors = searchable.getAllPossibleStates(n);
            //    foreach (State s in succerssors)
            //    {
            //        if (!closed.Contains(s) && !openContains(s))
            //        {
            //            addToOpenList(s);
            //        }
            //        else
            //        {
            //        }
            //    }
            //}
            return new Solution<T>(null);
        }

    }
}
