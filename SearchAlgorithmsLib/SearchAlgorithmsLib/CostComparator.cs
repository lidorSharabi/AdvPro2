using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class CostComparator<T> : Comparator<T>
    {
        public float compare(State<T> first, State<T> second)
        {
            return first.cost - second.cost;
        }
    }
}
