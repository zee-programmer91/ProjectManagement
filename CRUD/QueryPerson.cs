using Npgsql;
using ProjectManagement.Model;
using ProjectManagement.Database;
using ProjectManagement.utlis;

namespace ProjectManagement.CRUD
{
    public class QueryPerson
    {
        private static readonly DatabaseConnection databaseConnection = new DatabaseConnection();
        private static List<Person> persons;

        public static int GetPersonID(string name, string surname)
        {
            databaseConnection.OpenConnection();
            persons = new List<Person>();
            int personID = 0;

            try
            {
                string commandText = $"SELECT PERSON.person_id FROM PERSON WHERE person_name = @name and person_surname = @surname;";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("surname", surname);

                personID = (int)cmd.ExecuteScalar();
                Console.WriteLine($"Found the ID of {name} {surname}");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not get Person with Name '{name}' and Surname '{surname}'");
            }

            databaseConnection.DisposeConnection();
            return personID;
        }

        public static Person GetPersonByID(int id)
        {
            databaseConnection.OpenConnection();
            persons = new List<Person>();

            try
            {
                string commandText = $"SELECT * FROM PERSON WHERE PERSON_ID = @person_id";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", id);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = DatabaseReaders.ReadPerson(reader);
                    databaseConnection.DisposeConnection();
                    persons.Add(person);
                    return person;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not get Person with ID '{id}'");
            }

            databaseConnection.DisposeConnection();
            return new Person();
        }

        public static int GetAllPersons()
        {
            databaseConnection.OpenConnection();
            persons = new List<Person>();
            int result = 0;

            try
            {
                string commandText = $"SELECT * FROM PERSON WHERE DELETED = CAST(0 as bit);";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = DatabaseReaders.ReadPerson(reader);
                    persons.Add(person);
                    result++;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not all person from the Person table");
            }

            databaseConnection.DisposeConnection();
            return result;
        }

        public static int AddPerson(string name, string surname, string identityCode)
        {
            databaseConnection.OpenConnection();
            int result = 0;

            if (name == "" && surname == "")
            {
                return result;
            }

            try
            {
                string commandText = $"INSERT INTO PERSON (person_name, person_surname, identity_code) VALUES(@person_name,@person_surname,@identity_code);";

                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_name", name);
                cmd.Parameters.AddWithValue("person_surname", surname);
                cmd.Parameters.AddWithValue("identity_code", identityCode);
                result = cmd.ExecuteNonQuery();

                Console.WriteLine($"Saved person into the Person table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not save person to the Person table");
            }

            databaseConnection.DisposeConnection();
            return result;
        }

        public static int HardDeletingPerson(int person_id)
        {
            databaseConnection.OpenConnection();
            int result = 0;

            string commandText = $"DELETE FROM PERSON WHERE ID = @person_id)";

            try
            {
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);

                result = cmd.ExecuteNonQuery();

                Console.WriteLine($"DELETED PERSON WITH THE ID OF {person_id} FROM PERSON TABLE");
            } catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not delete person with ID {person_id}");
            }
            databaseConnection.DisposeConnection();
            return  result;
        }

        public static int SoftDeletingPerson(int person_id)
        {
            databaseConnection.OpenConnection();
            int result = 0;

            string commandText = $"UPDATE PERSON SET DELETED = CAST(1 AS BIT);";

            try
            {
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);

                result = cmd.ExecuteNonQuery();

                Console.WriteLine($"SOTF DELETED PERSON WITH THE ID OF {person_id} FROM PERSON TABLE");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not delete person with ID {person_id}");
            }
            databaseConnection.DisposeConnection();
            return result;
        }

        public static int UpdatePerson(int person_id, string person_name, string person_surname)
        {
            int result = 0;
            List<Person> persons = new List<Person>();
            Person person = new Person();
            person.personId = person_id;
            person.personName = person_name;
            person.personSurname = person_surname;

            string updateQuery = UpdateCreator.CreatePersonUpdateQuery(person);
            Console.WriteLine("query: " + updateQuery);
            
            return result;
        }

        public static int SoftDeletePerson(int person_id)
        {
            int result = 0;

            databaseConnection.OpenConnection();

            try
            {
                string commandText = $"UPDATE PERSON SET deleted = CAST(1 AS bit) WHERE person_id = @person_id;";

                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"Soft Deleted Person WITH ID {person_id} IN PERSON TABLE");

                databaseConnection.DisposeConnection();
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Person with ID {person_id} does not exist");
            }
            databaseConnection.DisposeConnection();
            return result;
        }

        public static int HardDeletePerson(int person_id)
        {
            int result = 0;

            databaseConnection.OpenConnection();

            try
            {
                string commandText = $"DELETE FROM PERSON WHERE person_id = @person_id;";

                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"Hard Deleted Person WITH ID {person_id} IN PERSON TABLE\nRows affected: {result}");

                databaseConnection.DisposeConnection();
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Person with ID {person_id} does not exist");
            }
            databaseConnection.DisposeConnection();
            return result;
        }

        public static void CloseOpenConnection()
        {
            databaseConnection.DisposeConnection();
        }
    }
}
