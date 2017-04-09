using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    interface ISearcher<T>
    {
        Solution search(ISearchable<T> searchable);
        int getNumberOfNodesEvaluated();
    }
}
