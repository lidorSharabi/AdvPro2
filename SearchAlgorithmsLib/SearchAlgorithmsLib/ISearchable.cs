using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// searchable object interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// get the initialize state, the state to start from
        /// </summary>
        /// <returns>the initialize state, the state to start from</returns>
        State<T> GetInitializeState();
        /// <summary>
        /// get the goal state
        /// </summary>
        /// <returns>the goal state</returns>
        State<T> GetGoalState();
        /// <summary>
        /// look for all states that can be accessed directy from "s" (by one move)
        /// </summary>
        /// <param name="s">the state that we want his accessible states</param>
        /// <returns>a list of state that can be accessed directy from "s" (by one move)</returns>
        List<State<T>> GetAllPossibleStates(State<T> s);
    }
}
