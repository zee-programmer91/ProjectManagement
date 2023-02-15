using LiveNiceApp;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.Model;

namespace ProjectManagement.Models
{
    internal class QueryContact
    {
        private static readonly DatabaseConnection databaseConnection = new DatabaseConnection();

        public static Contact[] GetContactByID(int contact_id)
        {
            databaseConnection.OpenConnection();
            List<Contact> contacts = new();

            try
            {
                string commandText = $"SELECT * FROM CONTACT WHERE contact_id = @contact_id;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("contact_id", contact_id);

                cmd.ExecuteNonQueryAsync();
                Console.WriteLine($"Selected all contacts from the CONTACT Table");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all contacts");
            }

            databaseConnection.DisposeConnection();
            return contacts.ToArray();
        }

        public static Contact[] GetAllContacts()
        {
            databaseConnection.OpenConnection();
            List<Contact> contacts = new();

            try
            {
                string commandText = $"SELECT * FROM CONTACT;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                cmd.ExecuteNonQueryAsync();
                Console.WriteLine($"Selected all contacts from the CONTACT Table");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all contacts");
            }

            databaseConnection.DisposeConnection();
            return contacts.ToArray();
        }

            public static void AddContact(Contact contact, NpgsqlConnection connection)
        {
            string commandText = $"INSERT INTO CONTACT (contact_id, person_id, email, cellphone_number) VALUES(@contact_id,@person_id,@email, @cellphone_number);";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("person_id", contact.person_id);
                cmd.Parameters.AddWithValue("email", contact.Email);
                cmd.Parameters.AddWithValue("cellphone_number", contact.cellphone_number);

                cmd.ExecuteNonQueryAsync();
                Console.WriteLine($"SAVED CONTACT WITH ID {contact.contact_id} INTO CONTACT TABLE");
            }
        }

        public static void DeleteContact(Contact contact) { }

        public static Contact[] UpdateContactEmail(int id, string email)
        {
            databaseConnection.OpenConnection();
            List<Contact> contacts = new();

            try
            {
                string commandText = $@"UPDATE CONTACT SET email = @email WHERE id = @id;";

                using (var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("id", id);

                    cmd.ExecuteNonQueryAsync();
                    Console.WriteLine($"UPDATED CONTACT EMAIL WITH ID {id} IN CONTACT TABLE");
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update CONTACT table with id {id}");
            }

            databaseConnection.DisposeConnection();
            return contacts.ToArray();
        }

        public static Contact[] UpdateContactCellphone(int id, string cellphone)
        {
            databaseConnection.OpenConnection();
            List<Contact> contacts = new();

            try
            {
                string commandText = $@"UPDATE CONTACT SET cellphone = @cellphone WHERE id = @id;";

                using (var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("cellphone", cellphone);
                    cmd.Parameters.AddWithValue("id", id);

                    cmd.ExecuteNonQueryAsync();
                    Console.WriteLine($"UPDATED CONTACT Cellphone WITH ID {id} IN CONTACT TABLE");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update CONTACT table with id {id}");
            }

            databaseConnection.DisposeConnection();
            return contacts.ToArray();
        }
    }
}
