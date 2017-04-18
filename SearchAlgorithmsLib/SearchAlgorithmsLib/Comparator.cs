using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface Comparator<T>
    {
        /*return 0 if equals, else return >0 if first is bigger than second,
        else return <0 if second is bigger than first*/
        float compare(State<T> first, State<T> second);
    }
}
