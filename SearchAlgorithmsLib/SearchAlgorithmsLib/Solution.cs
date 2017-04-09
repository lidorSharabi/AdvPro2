using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        private State<T> solution { get; set; }

        public Solution(State<T> solution)
        {
            this.solution = solution;
        }
    }
}
