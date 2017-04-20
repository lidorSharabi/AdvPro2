using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    interface ISearcher<T>
    {
        /// <summary>
        /// execute to search according the search algoritm
        /// </summary>
        /// <param name="searchable">obj that can be searched on, has functions initialize/goal state and get all possible states from specific state</param>
        /// <param name="comparator">has function copmare which helped the comparator param to determine who's better state to came from</param>
        /// <returns></returns>
        Solution<T> Search(ISearchable<T> searchable, Comparator<T> comparator = null);
        /// <summary>
        /// compute how much nodes evaluated while running "Search" function
        /// </summary>
        /// <returns>the solution according the search algoritm:
        /// path to the goal and the number of state that evaluated</returns>
        int GetNumberOfNodesEvaluated();
    }
}
