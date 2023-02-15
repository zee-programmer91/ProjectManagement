using Npgsql;
using System.Text.Json;
using ProjectManagement.Config;

namespace ProjectManagement.Database
{
    public class Database
    {
        private NpgsqlConnection connection {get;}
        private string ConnectionString = setConfiguration();

        public Database()
        {
            connection = new NpgsqlConnection(ConnectionString);
            setConfiguration();
        }

        public NpgsqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public static string setConfiguration()
        {
            string text = File.ReadAllText(@"D:\BBD_Training\C#\LiveNiceApp\databaseConfig.json");
            Console.WriteLine("text: "+ text);
            var configuration = JsonSerializer.Deserialize<Configuration>(text);
            string connectionString = $"Host={configuration.Host};Username={configuration.Username};Password={configuration.Password};Database={configuration.DatabaseName};";

            Console.WriteLine($"Host: {configuration.Host}");
            Console.WriteLine($"Username: {configuration.Username}");
            Console.WriteLine($"Password: {configuration.Password}");

            return connectionString;
        }
    }
}
