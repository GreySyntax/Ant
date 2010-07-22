using System;

using MySql.Data.MySqlClient;

using AntLibrary.Util;

namespace AntLibrary.Storage
{
    class DatabaseClient : IDisposable
    {
        private MySqlConnection _connection;
        private MySqlCommand _command;
        private uint _id;

        public DatabaseClient(uint id, MySqlConnection connection)
        {
            _id = id;
            _connection = connection;
        }

        public void Dispose()
        {
            if (_command != null)
            {
                _command.Dispose();
                _command = null;
            }

            //TODO Poke
        }

        public void AddParameter(string id, object value)
        {
            try
            {
                if (_command == null)
                    _command = _connection.CreateCommand();

                _command.Parameters.Add(new MySqlParameter(id, value));
            }
            catch (Exception e)
            {
                Logging.LogEvent("DatabaseClient", string.Format("Error adding parameter to client #{0} : {1}", _id, e.Message), Logging.ELogLevel.ERROR);
            }
        }

        public void Close()
        {
            if (_command != null)
            {
                _command.Dispose();
                _command = null;
            }

            _connection.Close();
        }
    }
}
