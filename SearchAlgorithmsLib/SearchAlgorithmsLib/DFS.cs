using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    
    public class DFS<T> : Searcher<T>
    {
        public override Solution<T> search(ISearchable<T> searchable)
        {
            addToOpenList(searchable.getInitializeState());
            return new Solution<T>(null);
        }
    }
}
