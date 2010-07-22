using System;
using System.Text;
using System.Threading;

using AntLibrary.Util;

namespace AntLibrary
{
    public class Ant
    {
        //Instance
        private static Ant _ant;

        //Objects
        private DateTime _started;
        private bool _running = true;
        
        //Managers
        public static Ant Get()
        {
            if (_ant == null)
                _ant = new Ant();
            return _ant;
        }

        //Getters & Setters
        public bool IsRunning
        {
            get
            {
                return _running;
            }
            set
            {
                _running = value;

                if (!_running)
                {
                    //Shutdown the application
                    DateTime shutdown = DateTime.Now;

                    //Debug information

                    Thread.Sleep(1000);
                    Environment.Exit(1);
                }
            }
        }

        //Methods
        public void StartAnt()
        {
            Console.Title = "Ant IRCd";
            Console.CursorVisible = false;

            Console.WindowHeight = 50;
            Console.WindowWidth = 100;

            Logging.WriteRaw("");
            Logging.WriteRaw("***");
            Logging.WriteRaw("* Ant IRCd");
            Logging.WriteRaw("* <puppet> a woman can fake an orgasm, but it takes a man to fake an entire relationship");
            Logging.WriteRaw("**");
            Logging.WriteRaw("");

            _started = DateTime.Now;
            Logging.LogEvent("AntCore", string.Format("Startup time {0}", _started.ToString()), Logging.ELogLevel.INFO);

            Thread.CurrentThread.Name = "AppThread";
            Logging.LogEvent("AntCore", "Created AppThread", Logging.ELogLevel.INFO);
        }
    }
}
