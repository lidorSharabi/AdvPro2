using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public interface ISearcher
    {
        Solution search(ISearchable searchable);
        int getNumberOfNodesEvaluated();
    }
}
