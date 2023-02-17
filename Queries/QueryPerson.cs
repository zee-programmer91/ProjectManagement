using Npgsql;
using ProjectManagement.Model;
using ProjectManagement.Database;
using ProjectManagement.utlis;

namespace ProjectManagement.Queries
{
    public class QueryPerson
    {
        private static readonly DatabaseConnection databaseCnnection = new DatabaseConnection();
        private static List<Person> persons;

        public static int GetPersonID(string name, string surname)
        {
            databaseCnnection.OpenConnection();
            persons = new List<Person>();
            int personID = 0;

            try
            {
                string commandText = $"SELECT PERSON.person_id FROM PERSON WHERE person_name = @name and person_surname = @surname;";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("surname", surname);

                personID = (int)cmd.ExecuteScalar();
                Console.WriteLine($"Found the ID of {name} {surname}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not get Person with Name '{name}' and Surname '{surname}'");
            }

            databaseCnnection.DisposeConnection();
            return personID;
        }

        public static int GetPersonByID(int id)
        {
            databaseCnnection.OpenConnection();
            persons = new List<Person>();
            int result = 0;

            try
            {
                string commandText = $"SELECT * FROM PERSON WHERE PERSON_ID = @person_id";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", id);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = DatabaseReaders.ReadPerson(reader);
                    databaseCnnection.DisposeConnection();
                    persons.Add(person);
                    result = 1;
                    return result;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not get Person with ID '{id}'");
            }

            databaseCnnection.DisposeConnection();
            return result;
        }

        public static int GetAllPersons()
        {
            databaseCnnection.OpenConnection();
            persons = new List<Person>();
            int result = 0;

            try
            {
                string commandText = $"SELECT * FROM PERSON WHERE DELETED = CAST(0 as bit);";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = DatabaseReaders.ReadPerson(reader);
                    persons.Add(person);
                }
                result = 1;
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not all person from the Person table");
            }

            databaseCnnection.DisposeConnection();
            return result;
        }

        public static int AddPerson(string name, string surname, string identityCode)
        {
            databaseCnnection.OpenConnection();
            int result = 0;

            try
            {
                string commandText = $"INSERT INTO PERSON (person_name, person_surname, identity_code) VALUES(@person_name,@person_surname,@identity_code);";

                using var cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                cmd.Parameters.AddWithValue("person_name", name);
                cmd.Parameters.AddWithValue("person_surname", surname);
                cmd.Parameters.AddWithValue("identity_code", identityCode);
                result = cmd.ExecuteNonQuery();

                Console.WriteLine($"Saved person into the Person table");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not save person to the Person table");
            }

            databaseCnnection.DisposeConnection();
            return result;
        }

        public static int DeletePerson(int person_id)
        {
            databaseCnnection.OpenConnection();
            int result = 0;

            string commandText = $"DELETE FROM PERSON WHERE ID = @person_id)";

            using var cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
            cmd.Parameters.AddWithValue("person_id", person_id);

            result = cmd.ExecuteNonQuery();

            Console.WriteLine($"DELETED PERSON WITH THE ID OF {person_id} FROM PERSON TABLE");
            databaseCnnection.DisposeConnection();
            return  result;
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

            databaseCnnection.OpenConnection();

            try
            {
                string commandText = $"UPDATE PERSON SET deleted = CAST(1 AS bit) WHERE person_id = @person_id;";

                using var cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"Soft Deleted Person WITH ID {person_id} IN PERSON TABLE");

                databaseCnnection.DisposeConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Person with ID {person_id} does not exist");
            }
            databaseCnnection.DisposeConnection();
            return result;
        }

        public static int HardDeletePerson(int person_id)
        {
            int result = 0;

            databaseCnnection.OpenConnection();

            try
            {
                string commandText = $"DELETE FROM PERSON WHERE person_id = @person_id;";

                using var cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"Hard Deleted Person WITH ID {person_id} IN PERSON TABLE\nRows affected: {result}");

                databaseCnnection.DisposeConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Person with ID {person_id} does not exist");
            }
            databaseCnnection.DisposeConnection();
            return result;
        }
    }
}
