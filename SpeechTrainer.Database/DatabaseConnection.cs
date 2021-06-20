using System;
using System.Data.SqlClient;

namespace SpeechTrainer.Database
{
    public sealed class DatabaseConnection
    {
        // private readonly string connectionString =
        //     @"Data Source=MSI;Initial Catalog=VKR_SpeechModule_TKSG;persist security info=True;Integrated Security=SSPI;MultipleActiveResultSets=True";
        private readonly string connectionString =
            @"Data Source=MSI;Initial Catalog=VKR_SpeechModule_TKSG;persist security info=True;Integrated Security=SSPI;MultipleActiveResultSets=True;User ID=ia_norm_patsan_8@mail.ru;Password=Otsosi_potom_prosi1";

        public SqlConnection Connection { get; }

        public Exception RootException { get; set; }

        private DatabaseConnection()
        {
            Connection = new SqlConnection(connectionString);
        }

        private static readonly Lazy<DatabaseConnection> _lazy =
            new Lazy<DatabaseConnection>(() => new DatabaseConnection());

        public static DatabaseConnection Source => _lazy.Value;

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