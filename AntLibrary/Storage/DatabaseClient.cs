using System;

using MySql.Data.MySqlClient;

namespace AntLibrary.Storage
{
    class DatabaseClient : IDisposable
    {
        private MySqlConnection _connection;
        private uint _id;

        public DatabaseClient(uint id, MySqlConnection connection)
        {
            _id = id;
            _connection = connection;
        }

        public void Dispose()
        {
            //TODO Poke database manager "IM DONE"
        }
    }
}
