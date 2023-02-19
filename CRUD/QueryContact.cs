using Model;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.utlis;
using static ProjectManagement.Database.DatabaseActions;

namespace ProjectManagement.CRUD
{
    public class QueryContact : DatabaseActionsBridge
    {
        public static new object GetByID(int ID)
        {
            try
            {
                string commandText = $"SELECT * FROM CONTACT WHERE contact_id = @contact_id;";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("contact_id", ID);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = DatabaseReaders.ReadContact(reader);
                    Console.WriteLine($"Selected contact with ID {ID} from the CONTACT Table");

                    return contact;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not get contact: {e.Message}");
            }

            return new Contact();
        }

        public static new List<Contact> GetAll()
        {
            List<Contact> contacts = new();

            try
            {
                string commandText = $"SELECT * FROM CONTACT;";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = DatabaseReaders.ReadContact(reader);
                    contacts.Add(contact);
                }
                Console.WriteLine($"Selected all contacts from the CONTACT Table");

                return contacts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all contacts");
            }

            return contacts;
        }

        public static new DatabaseActionsResponses InsertEntry(object newEntry)
        {
            int result = 0;
            Contact newContact = (Contact)newEntry;

            try
            {
                string commandText = $"INSERT INTO CONTACT (person_id, email, cellphone_number) VALUES(@person_id,@email, @cellphone_number);";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("person_id", newContact.PersonID);
                cmd.Parameters.AddWithValue("email", newContact.email);
                cmd.Parameters.AddWithValue("cellphone_number", newContact.cellphoneNumber);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"Saved contact of person with ID {newContact.personID} INTO CONTACT table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not add contact details to the contact table for the person with the ID {newContact.personID}");
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
                string commandText = $"UPDATE CONTACT SET deleted = CAST(1 AS bit) WHERE contact_id = @contact_id;";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("contact_id", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Soft Deleted Contact WITH ID {ID} IN CONTACT TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Person with ID {ID} could not be deleted");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteEntryByID(int ID)
        {
            int result = 0;

            try
            {
                string commandText = $"DELETE FROM CONTACT WHERE contact_id = @contact_id;";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("contact_id", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Hard Deleted Contact WITH ID {ID} IN CONTACT TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Person with ID {ID} could not be deleted");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses UpdateEntryByID(int ID, object updateEntry)
        {
            int result = 0;

            Contact updateContact = (Contact)updateEntry;

            Dictionary<string, bool> columnStatus = new Dictionary<string, bool>
            {
                { "cellphone_number", false },
                { "email", false },
            };

            if (updateContact.cellphoneNumber == "")
            {
                columnStatus["cellphone_number"] = true;
            }

            if (updateContact.email == "")
            {
                columnStatus["email"] = true;
            }

            Dictionary<string, object> updateColumns = new Dictionary<string, object>
            {
                { "email", updateContact.email },
                { "cellphone_number", updateContact.cellphoneNumber }
            };

            string updateStatement = UpdateCreator.CreateUpdateQuery("Contact", updateColumns, "contact_id");

            try
            {
                using var cmd = new NpgsqlCommand(updateStatement, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("contact_id", ID);

                foreach (KeyValuePair<string, bool> entry in columnStatus)
                {
                    switch (entry.Value)
                    {
                        case false:
                            Console.WriteLine($"{entry.Key} - {updateColumns[entry.Key]}");
                            cmd.Parameters.AddWithValue($"{entry.Key}", updateColumns[entry.Key]);
                            break;
                    }
                }

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"UPDATE TO CONTACT_ID {ID} INTO CONTACT TABLE");
            } catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Could not update contact details of contact ID {ID}");
            }

            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }
    }
}
