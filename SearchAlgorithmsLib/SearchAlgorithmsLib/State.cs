using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        private T state { get; }
        public float cost { get; set; }
        public State<T> cameFrom { get; set; }

        private State(T state)
        {
            this.state = state;
        }

        public T Getstate()
        {
            return state;
        }

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

        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }


    }
}
