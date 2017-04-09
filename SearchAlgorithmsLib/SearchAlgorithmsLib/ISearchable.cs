using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        State<T> getInitializeState();
        State<T> getGoalState();
        List<State<T>> getAllPossibleStates(State<T> s);
    }
}
