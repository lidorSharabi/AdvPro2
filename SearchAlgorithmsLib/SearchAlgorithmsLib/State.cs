using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        private string state; // the state represented by a string
        public double cost { get; set; } // cost to reach this state (set by a setter)
        public State<T> cameFrom { get; set; } // the state we came from to this state (setter)

        public State(string state) // CTOR
        {
            this.state = state;
        }

        public T getstate()
        {
            return state;
        }

        public static class StatePool
        {
            private static Dictionary<int, State<T>> pool= new Dictionary<int,State<T>>();
            private static HashSet<T> hashState = new HashSet<T>();

            public static State<T> getState(T state)
            {
                lock (hashState)
                {
                    if (!hashState.Contains(state))
                    {
                        State<T> stateToAdd = new State<T>(state);
                        hashState.Add(state);
                        int hash = state.GetHashCode();
                        pool.Add(hash, stateToAdd);
                        return pool[hash];
                    }
                    else
                    {
                        int hash = state.GetHashCode();
                        return pool[hash];
                    }
                }
            }

        }

        // we overload Object's Equals method
        public bool Equals(State s)
        {
            return state.Equals(s.state);
        }

        State getCameFrom() { return cameFrom; }

        void setCameFrom(State father) { this.cameFrom = father; }
}
}
