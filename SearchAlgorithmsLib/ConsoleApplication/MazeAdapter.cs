using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace ConsoleApplication
{
    class MazeAdapter : ISearchable
    //TODO - is MazeAdapter should implemnets Searchable?
    // can't add SearchAlgorithmsLib as a refernece
    {
        public List<State> getAllPossibleStates(State s)
        {
            throw new NotImplementedException();
        }

        public State getGoalState()
        {
            throw new NotImplementedException();
        }

        public State getInitializeState()
        {
            throw new NotImplementedException();
        }
    }
}
