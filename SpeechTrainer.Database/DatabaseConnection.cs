using System;
using System.Data.SqlClient;

namespace SpeechTrainer.Database
{
    public sealed class DatabaseConnection
    {
        private readonly string connectionString =
            @"Data Source=MSI;Initial Catalog=Modelov_DiaryApp;persist security info=True;Integrated Security=SSPI";

        public SqlConnection Connection { get; }

        private DatabaseConnection()
        {
            Connection = new SqlConnection(connectionString);
        }

        private static readonly Lazy<DatabaseConnection> lazy =
            new Lazy<DatabaseConnection>(() => new DatabaseConnection());

        public static DatabaseConnection Source => lazy.Value;

        public SqlConnection OpenConnection()
        {
            Connection.Open();
            return Connection;
        }

        public void CloseConnection()
        {
            Connection.Close();
        }
    }
}