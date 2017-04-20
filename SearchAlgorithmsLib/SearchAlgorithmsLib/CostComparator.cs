using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class CostComparator<T> : Comparator<T>
    {
        /// <summary>
        /// compare which state is better according their cost property
        /// </summary>
        /// <param name="first">first state</param>
        /// <param name="second">second state</param>
        /// <returns>return first.cost - second.cost</returns>
        public float Compare(State<T> first, State<T> second)
        {
            return first.cost - second.cost;
        }
    }
}
