using LiveNiceApp;
using Model;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.Models;
using ProjectManagement.utlis;
using static ProjectManagement.Models.DatabaseActions;

namespace ProjectManagement.CRUD
{
    public class QueryContact : DatabaseActionsBridge
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0090:Use 'new(...)'", Justification = "<Pending>")]
        private static readonly DatabaseConnection databaseConnection = new DatabaseConnection();

        public static new object GetByID(int ID)
        {
            databaseConnection.OpenConnection();
            try
            {
                string commandText = $"SELECT * FROM CONTACT WHERE contact_id = @contact_id;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("contact_id", ID);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = DatabaseReaders.ReadContact(reader);
                    Console.WriteLine($"Selected contact with ID {ID} from the CONTACT Table");
                    databaseConnection.DisposeConnection();

                    return contact;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not get contact: {e.Message}");
            }

            databaseConnection.DisposeConnection();
            return new Contact();
        }

        public static new object GetAll()
        {
            databaseConnection.OpenConnection();
            List<Contact> contacts = new();

            try
            {
                string commandText = $"SELECT * FROM CONTACT;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = DatabaseReaders.ReadContact(reader);
                    contacts.Add(contact);
                }
                Console.WriteLine($"Selected all contacts from the CONTACT Table");
                databaseConnection.DisposeConnection();

                return contacts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all contacts");
            }

            databaseConnection.DisposeConnection();
            return contacts;
        }

        public static new DatabaseActionsResponses InsertEntry(int ID, object newEntry)
        {
            databaseConnection.OpenConnection();
            int result = 0;
            Contact newContact = (Contact)newEntry;

            try
            {
                string commandText = $"INSERT INTO CONTACT (person_id, email, cellphone_number) VALUES(@person_id,@email, @cellphone_number);";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("person_id", ID);
                cmd.Parameters.AddWithValue("email", newContact.email);
                cmd.Parameters.AddWithValue("cellphone_number", newContact.cellphoneNumber);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"Saved contact of person with ID {ID} INTO CONTACT table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not add contact details to the contact table for the person with the ID {ID}");
            }

            databaseConnection.DisposeConnection();
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteAll()
        {
            return DatabaseActionsResponses.Success;
        }

        public static new DatabaseActionsResponses SoftDeleteEntryByID(int ID)
        {
            int result = 0;

            databaseConnection.OpenConnection();

            try
            {
                string commandText = $"UPDATE CONTACT SET deleted = CAST(1 AS bit) WHERE contact_id = @contact_id;";

                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("contact_id", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Soft Deleted Contact WITH ID {ID} IN CONTACT TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Person with ID {ID} could not be deleted");
            }
            databaseConnection.DisposeConnection();
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteEntryByID(int ID)
        {
            databaseConnection.OpenConnection();
            int result = 0;

            try
            {
                string commandText = $"DELETE FROM CONTACT WHERE contact_id = @contact_id;";

                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("contact_id", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Hard Deleted Contact WITH ID {ID} IN CONTACT TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Person with ID {ID} could not be deleted");
            }
            databaseConnection.DisposeConnection();
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses UpdateEntryByID(int ID, object updateEntry)
        {
            databaseConnection.OpenConnection();
            int result = 0;
            Contact newContact = (Contact)updateEntry;

            try
            {
                string commandText;

                if (newContact.cellphoneNumber == "" && newContact.email == "")
                {
                    databaseConnection.DisposeConnection();
                    return DatabaseActionsResponses.FieldEmpty;
                }
                else if (newContact.cellphoneNumber != "" && newContact.email == "")
                {
                    commandText = $"UPDATE CONTACT SET CELLPHONE_NUMBER = @cellphoneNumber WHERE contact_id = @id;";
                    using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                    cmd.Parameters.AddWithValue("cellphoneNumber", newContact.cellphoneNumber);
                    cmd.Parameters.AddWithValue("id", ID);
                    result = cmd.ExecuteNonQuery();
                }
                else if (newContact.cellphoneNumber == "" && newContact.email != "")
                {
                    commandText = $"UPDATE CONTACT SET EMAIL = @email WHERE contact_id = @id;";
                    using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                    cmd.Parameters.AddWithValue("email", newContact.email);
                    cmd.Parameters.AddWithValue("id", ID);
                    result = cmd.ExecuteNonQuery();
                }
                else if (newContact.cellphoneNumber != "" && newContact.email != "")
                {
                    commandText = $"UPDATE CONTACT SET EMAIL = @email, SET CELLPHONE_NUMBER = @cellphoneNumber WHERE contact_id = @id;";
                    using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                    cmd.Parameters.AddWithValue("email", newContact.email);
                    cmd.Parameters.AddWithValue("cellphoneNumber", newContact.cellphoneNumber);
                    cmd.Parameters.AddWithValue("id", ID);
                    result = cmd.ExecuteNonQuery();
                }

                Console.WriteLine($"UPDATED CONTACT EMAIL WITH ID {ID} IN CONTACT TABLE");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not update CONTACT table with id {ID}");
            }

            databaseConnection.DisposeConnection();
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static void CloseOpenConnection()
        {
            databaseConnection.DisposeConnection();
        }
    }
}
