using Npgsql;
using ProjectManagement.Model;
using ProjectManagement.Database;

using System.Collections.Generic;
using System;

namespace LiveNiceApp
{
    internal class QueryPerson
    {
        private static Database database = new Database();

        public static Person[] GetPersonByID(int id)
        {
            database.OpenConnection();
            List<Person> persons = new List<Person>();

            string commandText = $"SELECT * FROM PERSON WHERE PERSON_ID = @person_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, database.GetConnection()))
            {
                cmd.Parameters.AddWithValue("person_id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        Person person = ReadPerson(reader);
                        database.Dispose();
                        persons.Add(person);
                        return persons.ToArray();
                    }
            }
            database.Dispose();
            return persons.ToArray();
        }

        public static Person[] GetAllPersons()
        {
            database.OpenConnection();
            List<Person> persons = new List<Person>();

            string commandText = $"SELECT * FROM PERSON";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, database.GetConnection()))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        Person person = ReadPerson(reader);
                        persons.Add(person);
                    }
            }
            database.Dispose();
            return persons.ToArray();
        }

        private static Person ReadPerson(NpgsqlDataReader reader)
        {
            int id = (int)(reader["person_id"] as int?);
            string? name = reader["person_name"] as string;
            string? surname = reader["person_surname"] as string;
            string? identity = reader["identity_code"] as string;

            Person person = new Person()
            {
                person_id = id,
                person_name = name,
                person_surname = surname,
                identity_code = identity,
            };
            return person;
        }

        public static void AddPerson(Person person, NpgsqlConnection connection)
        {
            string commandText = $"INSERT INTO PERSON (person_name, person_surname, identity_code) VALUES(@person_name,@person_surname,@identity_code);";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("person_name", person.person_name);
                cmd.Parameters.AddWithValue("person_surname", person.person_surname);
                cmd.Parameters.AddWithValue("identity_code", person.identity_code);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"SAVED {person.person_name} INTO PERSON TABLE");
            }
        }

        public static void DeletePerson(Person person, NpgsqlConnection connection)
        {
            string commandText = $"DELETE FROM PERSON WHERE ID=@person_id)";
            using (var cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("person_id", person.person_id);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"DELETED {person.person_name} FROM PERSON TABLE");
            }
        }

        public static void UpdatePerson(Person person, NpgsqlConnection connection)
        {
            try
            {
                string commandText = $@"UPDATE PERSON SET person_name = @person_name WHERE id = @person_id;";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("person_id", person.person_id);
                    cmd.Parameters.AddWithValue("person_name", person.person_name);
                    cmd.Parameters.AddWithValue("person_surname", person.person_surname);
                    cmd.Parameters.AddWithValue("identity_code", person.identity_code);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"UPDATED PERSON WITH ID {person.person_id} IN PERSON TABLE");
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Person with ID {person.person_id} does not exist");
            }
        }
    }
}
