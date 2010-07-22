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
            AntLibrary.Ant.Get().StartAnt();

            AntLibrary.Ant.Get().IsRunning = false;
        }
    }
}
