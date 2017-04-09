using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class State
    {
        private string state; // the state represented by a string
        private double cost; // cost to reach this state (set by a setter)
        private State cameFrom;// the state we came from to this state (setter)

        public State(string state) // CTOR
        {
            this.state = state;
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
