using Npgsql;
using ProjectManagement.Model;
using ProjectManagement.Database;
using ProjectManagement.utlis;
using static ProjectManagement.Database.DatabaseActions;
using Model;

namespace ProjectManagement.CRUD
{
    public class QueryPerson : DatabaseActionsBridge
    {
        private static List<Person> persons;

        public static int GetPersonID(string name, string surname)
        {
            persons = new List<Person>();
            int personID = 0;

            try
            {
                string commandText = $"SELECT PERSON.person_id FROM PERSON WHERE person_name = @name and person_surname = @surname;";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("surname", surname);

                personID = (int)cmd.ExecuteScalar();
                Console.WriteLine($"Found the ID of {name} {surname}");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not get Person with Name '{name}' and Surname '{surname}'");
            }

            return personID;
        }

        public static new Person GetByID(int id)
        {
            persons = new List<Person>();

            try
            {
                string commandText = $"SELECT * FROM PERSON WHERE PERSON_ID = @person_id";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", id);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = DatabaseReaders.ReadPerson(reader);
                    persons.Add(person);
                    return person;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not get Person with ID '{id}'");
            }
            return new Person();
        }

        public static new List<Person> GetAll()
        {
            persons = new List<Person>();

            try
            {
                string commandText = $"SELECT * FROM PERSON;";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = DatabaseReaders.ReadPerson(reader);
                    persons.Add(person);
                }
                Console.WriteLine($"Selected all persons from the PERSON Table");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not all person from the Person table: {e.StackTrace}");
            }
            return persons;
        }

        public static new DatabaseActionsResponses InsertEntry(object newEntry)
        {
            int result = 0;
            Person newPerson = (Person)newEntry;

            try
            {
                string commandText = $"INSERT INTO PERSON (person_name, person_surname, identity_code) VALUES(@person_name,@person_surname,@identity_code);";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_name", newPerson.PersonName);
                cmd.Parameters.AddWithValue("person_surname", newPerson.PersonSurname);
                cmd.Parameters.AddWithValue("identity_code", newPerson.identityCode);
                result = cmd.ExecuteNonQuery();

                Console.WriteLine($"Saved person into the Person table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not save person to the Person table");
            }

            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteEntryByID(int ID)
        {
            int result = 0;

            string commandText = $"DELETE FROM PERSON WHERE ID = @person_id)";

            try
            {
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", ID);

                result = cmd.ExecuteNonQuery();

                Console.WriteLine($"DELETED PERSON WITH THE ID OF {ID} FROM PERSON TABLE");
            } catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not delete person with ID {ID}");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed; ;
        }

        public static new DatabaseActionsResponses SoftDeleteEntryByID(int ID)
        {
            int result = 0;

            string commandText = $"UPDATE PERSON SET DELETED = CAST(1 AS BIT);";

            try
            {
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", ID);

                result = cmd.ExecuteNonQuery();

                Console.WriteLine($"SOTF DELETED PERSON WITH THE ID OF {ID} FROM PERSON TABLE");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not delete person with ID {ID}");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses UpdateEntryByID(int ID, object updateEntry)
        {
            int result = 0;
            Person updatePerson = (Person)updateEntry;

            Dictionary<string, bool> columnStatus = new Dictionary<string, bool>
            {
                { "person_name", false },
                { "person_surname", false },
                { "identity_code", false },
            };

            if (updatePerson.personName == "")
            {
                columnStatus["person_name"] = true;
            }

            if (updatePerson.personSurname == "")
            {
                columnStatus["person_surname"] = true;
            }

            if (updatePerson.identityCode == "")
            {
                columnStatus["identity_code"] = true;
            }

            Dictionary<string, object> updateColumns = new Dictionary<string, object>
            {
                { "person_name", updatePerson.personName },
                { "person_surname", updatePerson.personSurname },
                { "identity_code", updatePerson.identityCode }
            };

            string updateStatement = UpdateCreator.CreateUpdateQuery("Person", updateColumns, "person_id");
            if (updateStatement == "")
            {
                return DatabaseActionsResponses.FieldEmpty;
            }

            Console.WriteLine($"updateStatement: {updateStatement}");
            try
            {
                using var cmd = new NpgsqlCommand(updateStatement, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", ID);

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Could not update contact details of contact ID {ID}");
            }

            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }
    }
}
