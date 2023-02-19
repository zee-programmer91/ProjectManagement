using Model;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.Models;
using ProjectManagement.utlis;
using static ProjectManagement.Database.DatabaseActions;

namespace ProjectManagement.CRUD
{
    public class QueryPersonAccess : DatabaseActionsBridge
    {
        public static new PersonAccess GetByID(int ID)
        {
            try
            {
                string commandText = $"SELECT * FROM PERSON_ACCESS WHERE PERSON_ID = @personID;";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("personID", ID);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PersonAccess personAccess = DatabaseReaders.ReadPersonAccess(reader);
                    Console.WriteLine($"Selected person access with ID {ID} from the PERSON_ACESS Table");

                    return personAccess;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not get person access: {e.Message}");
            }

            return new PersonAccess();
        }

        public static new List<PersonAccess> GetAll()
        {
            List<PersonAccess> contacts = new();

            try
            {
                string commandText = $"SELECT * FROM PERSON_ACCESS;";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PersonAccess personAccess = DatabaseReaders.ReadPersonAccess(reader);
                    contacts.Add(personAccess);
                }
                Console.WriteLine($"Selected all person access from the PERSON_ACCESS Table");

                return contacts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all person access");
            }

            return contacts;
        }

        public static new DatabaseActionsResponses InsertEntry(object newEntry)
        {
            int result = 0;
            PersonAccess newPersonAccess = (PersonAccess)newEntry;

            try
            {
                string commandText = $"INSERT INTO PERSON_ACCESS (person_id, access_id) VALUES(@person_id, @access_id);";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("person_id", newPersonAccess.PersonID);
                cmd.Parameters.AddWithValue("access_id", newPersonAccess.accessTypeID);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"Saved person access of person with ID {newPersonAccess.personID} INTO PERSON_ACCESS table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not add person access to the person acess table for the person with the ID {newPersonAccess.personID}");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteAll()
        {
            return DatabaseActionsResponses.Success;
        }

        public static new DatabaseActionsResponses SoftDeleteEntryByID(int ID)
        {
            int result = 0;

            try
            {
                string commandText = $"UPDATE PERSON_ACCESS SET deleted = CAST(1 AS bit) WHERE person_access_id = @person_access_id;";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_access_id", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Soft Deleted PersonAccess WITH ID {ID} IN PERSON_ACCESS TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Person with personAccessID {ID} could not be deleted");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteEntryByID(int ID)
        {
            int result = 0;

            try
            {
                string commandText = $"DELETE FROM PERSON_ACCESS WHERE person_access_id = @person_access_id;";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_access_id", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Hard Deleted PersonAccess WITH ID {ID} IN PERSON_ACCESS TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Person with PersonAccessID {ID} could not be deleted");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses UpdateEntryByID(int ID, object updateEntry)
        {
            int result = 0;

            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }
    }
}
