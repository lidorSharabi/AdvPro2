using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface Comparator<T>
    {
        /// <summary>
        /// compare which state is better
        /// </summary>
        /// <param name="first">the first state param will be compared to the second state param</param>
        /// <param name="second">the second state param will be compared to the first state param</param>
        /// <returns>return 0 - if the states are equals,
        /// return positive number - if the first state is better,
        /// otherwise return negative number</returns>
        float Compare(State<T> first, State<T> second);
    }
}
