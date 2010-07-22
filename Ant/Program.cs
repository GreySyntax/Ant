using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ant
{
    class Program
    {
        static void Main(string[] args)
        {
            AntLibrary.Ant.Get().StartAnt("127.0.0.1", 3306, "root", "password", "antdb", 8, 20);

            AntLibrary.Ant.Get().IsRunning = false;
        }
    }
}
