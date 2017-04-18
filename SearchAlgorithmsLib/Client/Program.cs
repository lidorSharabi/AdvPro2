using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "sad sdefd";
            Console.WriteLine(s[0]);
            Console.WriteLine(s[1]);
            Client client = new Client();
            client.start();
        }
    }
}
