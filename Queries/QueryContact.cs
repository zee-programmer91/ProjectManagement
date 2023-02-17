using LiveNiceApp;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.Model;
using ProjectManagement.utlis;

namespace ProjectManagement.Queries
{
    public class QueryContact
    {
        private static readonly DatabaseConnection databaseConnection = new DatabaseConnection();

        public static int GetContactByID(int contact_id)
        {
            databaseConnection.OpenConnection();
            int results = 0;

            try
            {
                string commandText = $"SELECT * FROM CONTACT WHERE contact_id = @contact_id;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("contact_id", contact_id);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = DatabaseReaders.ReadContact(reader);
                    databaseConnection.DisposeConnection();
                    results = 1;
                }
                Console.WriteLine($"Selected all contacts from the CONTACT Table");
                return results;
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not get all contacts");
            }

            databaseConnection.DisposeConnection();
            return results;
        }

        public static int GetAllContacts()
        {
            databaseConnection.OpenConnection();
            List<Contact> contacts = new();
            int results = 0;

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
                results = 1;
                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all contacts");
            }

            databaseConnection.DisposeConnection();
            return results;
        }

        public static int AddContact(int person_id, string cellphone, string email)
        {
            databaseConnection.OpenConnection();
            int results = 0;

            try
            {
                string commandText = $"INSERT INTO CONTACT (person_id, email, cellphone_number) VALUES(@person_id,@email, @cellphone_number);";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("person_id", person_id);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("cellphone_number", cellphone);

                results = cmd.ExecuteNonQuery();
                Console.WriteLine($"Saved contact of person with ID {person_id} INTO CONTACT table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not add contact details to the contact table for the person with the ID {person_id}");
            }

            databaseConnection.DisposeConnection();
            return results;
        }

        public static int SoftDeleteContact(int contact_id)
        {
            int result = 0;

            databaseConnection.OpenConnection();

            try
            {
                string commandText = $"UPDATE CONTACT SET deleted = CAST(1 AS bit) WHERE contact_id = @contact_id;";

                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("contact_id", contact_id);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Soft Deleted Contact WITH ID {contact_id} IN CONTACT TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Person with ID {contact_id} could not be deleted");
            }
            databaseConnection.DisposeConnection();
            return result;
        }

        public static int HardDeleteContact(int contact_id)
        {
            databaseConnection.OpenConnection();
            int result = 0;

            try
            {
                string commandText = $"DELETE FROM CONTACT WHERE contact_id = @contact_id;";

                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("contact_id", contact_id);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Hard Deleted Contact WITH ID {contact_id} IN CONTACT TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Person with ID {contact_id} could not be deleted");
            }
            databaseConnection.DisposeConnection();
            return result;
        }

        public static int UpdateContact(int id, string cellphoneNumber, string email)
        {
            databaseConnection.OpenConnection();
            int result = 0;

            try
            {
                string commandText;

                if (cellphoneNumber == "" && email == "")
                {
                    return result;
                }
                else if (cellphoneNumber != "" && email == "")
                {
                    commandText = $"UPDATE CONTACT SET CELLPHONE_NUMBER = @cellphoneNumber WHERE contact_id = @id;";
                    using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                    cmd.Parameters.AddWithValue("cellphoneNumber", cellphoneNumber);
                    cmd.Parameters.AddWithValue("id", id);
                    result = cmd.ExecuteNonQuery();
                }
                else if (cellphoneNumber == "" && email != "")
                {
                    commandText = $"UPDATE CONTACT SET EMAIL = @email WHERE contact_id = @id;";
                    using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("id", id);
                    result = cmd.ExecuteNonQuery();
                }
                else if (cellphoneNumber != "" && email != "")
                {
                    commandText = $"UPDATE CONTACT SET EMAIL = @email, SET CELLPHONE_NUMBER = @cellphoneNumber WHERE contact_id = @id;";
                    using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("cellphoneNumber", cellphoneNumber);
                    cmd.Parameters.AddWithValue("id", id);
                    result = cmd.ExecuteNonQuery();
                }

                Console.WriteLine($"UPDATED CONTACT EMAIL WITH ID {id} IN CONTACT TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update CONTACT table with id {id}");
            }

            databaseConnection.DisposeConnection();
            return result;
        }
    }
}
