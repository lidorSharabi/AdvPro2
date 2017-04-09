using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace ConsoleApplication
{
    class MazeAdapter<T> : ISearchable<T>
    //TODO - is MazeAdapter should implemnets Searchable?
    {
        //public MazeAdapter<T>(){}
        public List<State<T>> getAllPossibleStates(State<T> s)
        {
            throw new NotImplementedException();
        }

        public State<T> getGoalState()
        {
            throw new NotImplementedException();
        }

        public State<T> getInitializeState()
        {
            throw new NotImplementedException();
        }
    }
}
