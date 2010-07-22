using System;
using System.Collections.Generic;
using System.Data;

using MySql.Data.MySqlClient;

using AntLibrary.Util;

namespace AntLibrary.Storage
{
    public class DatabaseManager : IManager
    {
        private bool _running;

        private string _server;
        private uint _port;

        private string _user;
        private string _password;

        private string _name;

        private uint _min;
        private uint _max;

        private MySqlConnectionStringBuilder _connectionString;

        private Dictionary<uint, DatabaseClient> _clients;

        private List<uint> _busy;
        private List<uint> _temporary;

        private uint _nextID = 1;
        private object _wait;

        public string ConnectionString
        {
            get
            {
                return _connectionString.ToString();
            }
        }

        public DatabaseManager(string server, uint port, string user, string password, string name, uint min, uint max)
        {
            _server = server;
            _port = port;

            _user = user;
            _password = password;

            _name = name;

            _min = min;
            _max = max;
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
            if (_server.Length == 0 && _user.Length == 0 && _password.Length == 0 && _name.Length == 0)
            {
                Logging.LogEvent("DatabaseManager", "Invalid credentials (server/user/password/name).", Logging.ELogLevel.ERROR);
                return false;
            }

            _wait = new object();
            _connectionString = new MySqlConnectionStringBuilder();

            _connectionString.Server = _server;
            _connectionString.Port = _port;

            _connectionString.UserID = _user;
            _connectionString.Password = _password;
            _connectionString.Database = _name;

            _connectionString.MinimumPoolSize = _min;
            _connectionString.MaximumPoolSize = _max;

            _clients = new Dictionary<uint, DatabaseClient>();
            _busy = new List<uint>();
            _temporary = new List<uint>();

            uint e = 0;

            while (_min > _clients.Count && 5 >= e)
            {
                uint id = Connect(false);

                if (id == 0)
                    e++;
            }

            if (e >= 5)
            {
                SetRunning(false);
                return false;
            }

            SetRunning(true);
            return true;
        }

        public void StopManager()
        {
            uint[] clients = new uint[_clients.Count];
            _clients.Keys.CopyTo(clients, 0);

            foreach (uint id in clients)
                Stop(id);

            if (_running)
                SetRunning(false, true);
        }

        public uint Connect(bool temporary)
        {
            uint id = 0;

            try
            {
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                connection.Open();

                uint tries = 10;
                while (connection.State == ConnectionState.Connecting && tries > 0)
                {
                    //Lets hope the state changes!
                    tries--;
                }

                if (0 >= tries)
                {
                    connection.Close();
                    return 0;
                }

                lock (_wait)
                {
                    id = _nextID;

                    _clients.Add(id, new DatabaseClient(id, connection));

                    Logging.LogEvent("DatabaseManager", string.Format("Created DatabaseClient #{0}", id), Logging.ELogLevel.INFO);

                    if (temporary)
                        _temporary.Add(id);

                    _nextID++;
                }
            }
            catch (MySqlException e)
            {
                Logging.LogEvent("DatabaseManager", string.Format("Failed to create DatabaseClient with error: {0}", e.Message), Logging.ELogLevel.WARNING);

                if (id > 0)
                {
                    //Remove
                    Stop(id);
                }
            }

            return id;
        }

        public void Stop(uint id)
        {
            if (_clients.ContainsKey(id))
            {
                DatabaseClient client = _clients[id];
                _clients.Remove(id);

                client.Close();

                Logging.LogEvent("DatabaseManager", string.Format("Destoryed DatabaseClient #{0}", id), Logging.ELogLevel.INFO);

                if (_busy.Contains(id))
                    _busy.Remove(id);
                if (_temporary.Contains(id))
                    _temporary.Remove(id);
            }
        }
    }
}
