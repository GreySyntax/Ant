using System;
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
        private Storage.DatabaseManager _databaseManager;

        //Instance
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
                    StopAnt();
                }
            }
        }

        public Storage.DatabaseManager DatabaseManager
        {
            get
            {
                return _databaseManager;
            }
        }

        //Methods
        public void StartAnt(string server, uint port, string user, string password, string name, uint min, uint max)
        {
            Console.Title = "Ant IRCd";
            Console.CursorVisible = false;

            Console.WindowHeight = 50;
            Console.WindowWidth = 150;

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

            _databaseManager = new Storage.DatabaseManager(server, port, user, password, name, min, max);
            Logging.LogEvent("AntCore", "Created DatabaseManager", Logging.ELogLevel.INFO);
            _databaseManager.StartManager();
                        
        }

        public void StopAnt()
        {           
            _databaseManager.SetRunning(false);

            DateTime shutdown = DateTime.Now;

            Logging.LogEvent("AntCore", string.Format("Shutdown time {0}", shutdown.ToString()), Logging.ELogLevel.INFO);
            Logging.LogEvent("AntCore", string.Format("Uptime {0} ", shutdown.Subtract(_started)), Logging.ELogLevel.INFO);

            Thread.Sleep(1000);
            Environment.Exit(1);
        }
    }
}
