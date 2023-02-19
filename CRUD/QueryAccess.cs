using Model;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.Models;
using ProjectManagement.utlis;
using System.Collections.Generic;
using static ProjectManagement.Database.DatabaseActions;

namespace ProjectManagement.CRUD
{
    public class QueryAccess : DatabaseActionsBridge
    {
        public static new Access GetByID(int ID)
        {
            try
            {
                string commandText = $"SELECT * FROM ACCESS_TYPE WHERE access_type_id = @access_id;";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("access_id", ID);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Access access = DatabaseReaders.ReadAccess(reader);
                    Console.WriteLine($"Selected access with ID {ID} from the ACCESS_TYPE Table");

                    return access;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not get access: {e.Message}");
            }

            return new Access();
        }

        public static new List<Access> GetAll()
        {
            List<Access> accesses = new();

            try
            {
                string commandText = $"SELECT * FROM ACCESS_TYPE;";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Access access = DatabaseReaders.ReadAccess(reader);
                    accesses.Add(access);
                }
                Console.WriteLine($"Selected all accesses from the ACCESS_TYPE Table");

                return accesses;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all accesses");
            }

            return accesses;
        }

        public static new DatabaseActionsResponses InsertEntry(object newEntry)
        {
            int result = 0;
            Access newAccess = (Access)newEntry;

            try
            {
                string commandText = $"INSERT INTO ACCESS_TYPE (ACCESS_TYPE_NAME) VALUES(@access_type_name);";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("access_type_name", newAccess.accessName);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"Saved access name to access table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not add access name to the ACCESS table");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses UpdateEntryByID(int ID, object updateEntry)
        {
            int result = 0;
            Access updateAccess = (Access)updateEntry;

            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>
            {
                { "access_type_name", updateAccess.accessName }
            };

            string updateStatement = UpdateCreator.CreateUpdateQuery("Access_Type", keyValuePairs, "access_type_id");

            Console.WriteLine($"updateStatement: {updateStatement}");

           if (updateStatement == "")
            {
                return DatabaseActionsResponses.FieldEmpty;
            }

            using var cmd = new NpgsqlCommand(updateStatement, DatabaseConnection.GetConnection());

            cmd.Parameters.AddWithValue("access_type_id", ID);
            cmd.Parameters.AddWithValue("access_type_name", updateAccess.accessName);

            result = cmd.ExecuteNonQuery();
            Console.WriteLine($"Updated {result} row(s) in ACCESS_TYPE Table");

            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed; ;
        }

        public static new DatabaseActionsResponses SoftDeleteEntryByID(int ID)
        {
            int result = 0;

            try
            {
                string commandText = $"UPDATE ACCESS_TYPE SET deleted = CAST(1 AS bit) WHERE access_type_id = @access_type_id;";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("access_type_id", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Soft Deleted access WITH ID {ID} IN Access_Type TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Access with ID {ID} could not be deleted");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteEntryByID(int ID)
        {
            int result = 0;

            try
            {
                string commandText = $"DELETE FROM ACCESS_TYPE WHERE access_type_id = @access_type_id;";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("access_type_id", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Hard Deleted Access WITH ID {ID} IN ACCESS_TYPE TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - ACCESS with ID {ID} could not be deleted");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }
    }
}
