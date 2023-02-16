using Npgsql;
using ProjectManagement.Model;
using ProjectManagement.Database;
using System;


namespace LiveNiceApp
{
    internal class QueryPerson
    {
        private static readonly DatabaseConnection databaseCnnection = new DatabaseConnection();
        private static List<Person> persons;

        public static Person[] GetPersonByID(int id)
        {
            databaseCnnection.OpenConnection();
            persons = new List<Person>();

            try
            {
                string commandText = $"SELECT * FROM PERSON WHERE PERSON_ID = @person_id";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", id);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = ReadPerson(reader);
                    databaseCnnection.DisposeConnection();
                    persons.Add(person);
                    return persons.ToArray();
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get Person with ID '{id}'");
            }

            databaseCnnection.DisposeConnection();
            return persons.ToArray();
        }

        public static Person[] GetAllPersons()
        {
            databaseCnnection.OpenConnection();
            persons = new();

            try
            {
                string commandText = $"SELECT * FROM PERSON";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = ReadPerson(reader);
                    persons.Add(person);
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not all person from the Person table");
            }

            databaseCnnection.DisposeConnection();
            return persons.ToArray();
        }

        public static Person[] AddPerson(string name, string surname, string identityCode)
        {
            databaseCnnection.OpenConnection();

            try
            {
                string commandText = $"INSERT INTO PERSON (person_name, person_surname, identity_code) VALUES(@person_name,@person_surname,@identity_code);";

                using var cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                cmd.Parameters.AddWithValue("person_name", name);
                cmd.Parameters.AddWithValue("person_surname", surname);
                cmd.Parameters.AddWithValue("identity_code", identityCode);
                cmd.ExecuteNonQuery();

                Console.WriteLine($"Saved person into the Person table");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not save person to the Person table");
            }

            databaseCnnection.DisposeConnection();
            return GetAllPersons();
        }

        public static void DeletePerson(int person_id)
        {
            databaseCnnection.OpenConnection();

            string commandText = $"DELETE FROM PERSON WHERE ID = @person_id)";

            using var cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
            cmd.Parameters.AddWithValue("person_id", person_id);

            cmd.ExecuteNonQuery();

            Console.WriteLine($"DELETED PERSON WITH THE ID OF {person_id} FROM PERSON TABLE");
            databaseCnnection.DisposeConnection();
        }

        public static Person[] UpdatePersonName(int person_id, string person_name)
        {
            int result = 0;
            List<Person> persons = new List<Person>();

            try
            {
                databaseCnnection.OpenConnection();
            }
            catch (Exception)
            {
                databaseCnnection.DisposeConnection();
            }

            try
            {
                string commandText = $"UPDATE PERSON SET person_name = @person_name WHERE person_id = @person_id;";

                using var cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);
                cmd.Parameters.AddWithValue("person_name", person_name);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"UPDATED PERSON WITH ID {person_id} IN PERSON TABLE");

                databaseCnnection.DisposeConnection();
                persons.Add(GetPersonByID(person_id)[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Person with ID {person_id} does not exist");
            }
            databaseCnnection.DisposeConnection();
            return persons.ToArray();
        }

        public static Person[] UpdatePersonSurname(int person_id, string person_surname)
        {
            int result = 0;
            List<Person> persons = new List<Person>();

            try
            {
                databaseCnnection.OpenConnection();
            }
            catch (Exception)
            {
                databaseCnnection.DisposeConnection();
            }

            try
            {
                string commandText = $"UPDATE PERSON SET person_surname = @person_surname WHERE person_id = @person_id;";

                using var cmd = new NpgsqlCommand(commandText, databaseCnnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);
                cmd.Parameters.AddWithValue("person_surname", person_surname);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"UPDATED PERSON WITH ID {person_id} IN PERSON TABLE");

                databaseCnnection.DisposeConnection();
                persons.Add(GetPersonByID(person_id)[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Person with ID {person_id} does not exist");
            }
            databaseCnnection.DisposeConnection();
            return persons.ToArray();
        }

        private static Person ReadPerson(NpgsqlDataReader reader)
        {

            var tempId = reader["person_id"];
            int id = 0;

            switch (tempId != null)
            {
                case true:
                    id = (int)tempId;
                    break;
                case false:
                    return new Person();
            }

            var tempName = reader["person_name"] as string;
            string name;

            switch (tempName != null)
            {
                case true:
                    name = tempName;
                    break;
                case false:
                    name = "";
                    break;
            }

            var tempSurname = reader["person_surname"] as string;
            string surname;

            switch (tempSurname != null)
            {
                case true:
                    surname = tempSurname;
                    break;
                case false:
                    surname = "";
                    break;
            }

            var tempIdentity = reader["identity_code"] as string;
            string identity;

            switch (tempIdentity != null)
            {
                case true:
                    identity = tempIdentity;
                    break;
                case false:
                    identity = "";
                    break;
            }

            Person person = new()
            {
                personId = id,
                personName = name,
                personSurname = surname,
                identityCode = identity,
            };

            return person;
        }
    }
}
