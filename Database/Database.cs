using Npgsql;
using System.Text.Json;
using ProjectManagement.Config;

namespace ProjectManagement.Database
{
    public class Database
    {
        private NpgsqlConnection connection {get;}
        private readonly string ConnectionString = SetConfiguration();

        public Database()
        {
            connection = new NpgsqlConnection(ConnectionString);
        }

        public NpgsqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void DisposeConnection()
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
