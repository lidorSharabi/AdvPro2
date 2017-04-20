using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        /// <summary>
        /// the current state
        /// </summary>
        private T state { get; }
        /// <summary>
        /// the "price" that cost to get to this state
        /// </summary>
        public float cost { get; set; }
        /// <summary>
        /// the state thtat we came from, if it's null it means that this is the initialize state
        /// </summary>
        public State<T> cameFrom { get; set; }
        /// <summary>
        /// private constructort
        /// </summary>
        /// <param name="state">set this.state = "state"</param>
        private State(T state)
        {
            this.state = state;
        }
        /// <summary>
        /// return current state
        /// </summary>
        /// <returns>return current state</returns>
        public T Getstate()
        {
            return state;
        }
        /// <summary>
        /// pool of all passible states,
        /// call the private constructort if the state doesn't exist in the pool
        /// </summary>
        public static class StatePool
        {
            private static Dictionary<int, State<T>> pool = new Dictionary<int, State<T>>();
            private static HashSet<T> hashState = new HashSet<T>();

            public static State<T> GetState(T state)
            {
                lock (hashState)
                {
                    if (!hashState.Contains(state))
                    {
                        State<T> stateToAdd = new State<T>(state);
                        hashState.Add(state);
                        int hash = state.ToString().GetHashCode();
                        pool.Add(hash, stateToAdd);
                        return pool[hash];
                    }
                    else
                    {
                        int hash = state.ToString().GetHashCode();
                        return pool[hash];
                    }
                }
            }

        }
        /// <summary>
        /// check if the "state" is equal to the current state
        /// </summary>
        /// <param name="s">the "state" to be compared</param>
        /// <returns>true if the're equals, else false</returns>
        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }


    }
}
