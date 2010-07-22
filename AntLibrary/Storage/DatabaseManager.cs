using System;

using AntLibrary.Util;

namespace AntLibrary.Storage
{
    class DatabaseManager : IManager
    {
        private bool _running;

        private string _server;
        private uint _port;

        private string _user;
        private string _password;

        private string _name;

        public DatabaseManager(string server, uint port, string user, string password, string name)
        {
            _server = server;
            _port = port;

            _user = user;
            _password = password;

            _name = name;
        }

        public bool GetRunning()
        {
            return _running;
        }

        public void SetRunning(bool value)
        {
            SetRunning(value, false);
        }

        public void SetRunning(bool value, bool soft)
        {
            _running = value;

            if (!_running && !soft)
                StopManager();
        }

        public bool StartManager()
        {


            SetRunning(true);
            return true;
        }

        private void StopManager()
        {

            if (_running)
                SetRunning(false, true);
        }
    }
}
