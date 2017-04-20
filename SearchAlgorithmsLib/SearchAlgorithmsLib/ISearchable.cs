using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        State<T> GetInitializeState();
        State<T> GetGoalState();
        List<State<T>> GetAllPossibleStates(State<T> s);
    }
}
