using Npgsql;
using System.Text.Json;
using ProjectManagement.Config;

namespace ProjectManagement.Database
{
    public class DatabaseConnection
    {
        private static NpgsqlConnection connection;
        private static NpgsqlConnection Connection {get;}
        private static string ConnectionString = SetConfiguration();

        public static NpgsqlConnection GetConnection()
        {
            connection ??= new NpgsqlConnection();
            return connection;
        }

        public static void OpenConnection()
        {
            connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
        }

        public static void DisposeConnection()
        {
            connection.Close();
        }

        public static string SetConfiguration()
        {
            try
            {
                string text = File.ReadAllText(@"D:\BBD_Training\C#\LiveNiceApp\databaseConfig.json");
                var configuration = JsonSerializer.Deserialize<Configuration>(text);
                string connectionString = $"Host={configuration.Host};Username={configuration.Username};Password={configuration.Password};Database={configuration.DatabaseName};";

                return connectionString;
            } catch(Exception)
            {
                return "";
            }
        }
    }
}
