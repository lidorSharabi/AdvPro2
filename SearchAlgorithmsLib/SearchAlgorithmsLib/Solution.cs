using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        public List<State<T>> vertex { get; }
        public Solution(List<State<T>> list)
        {
            vertex = list;
        }
    }
}
