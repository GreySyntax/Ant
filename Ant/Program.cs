using System;

namespace Ant
{
	class Program
	{
		public static void Main (string[] args)
		{
			AntLibrary.Ant.Get().StartAnt("127.0.0.1", 3306, "root", "root", "antdb", 8, 20);
			AntLibrary.Ant.Get().IsRunning = false;
		}
	}
}

