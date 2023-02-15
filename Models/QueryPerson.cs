using Npgsql;
using ProjectManagement.Model;
using ProjectManagement.Database;

using System.Collections.Generic;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LiveNiceApp
{
    internal class QueryPerson
    {
        private static readonly Database database = new Database();

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
                        database.DisposeConnection();
                        persons.Add(person);
                        return persons.ToArray();
                    }
            }
            database.DisposeConnection();
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
            database.DisposeConnection();
            return persons.ToArray();
        }

        public static void AddPerson(Person person)
        {
            database.OpenConnection();

            string commandText = $"INSERT INTO PERSON (person_name, person_surname, identity_code) VALUES(@person_name,@person_surname,@identity_code);";

            using var cmd = new NpgsqlCommand(commandText, database.GetConnection());
            cmd.Parameters.AddWithValue("person_name", person.personName);
            cmd.Parameters.AddWithValue("person_surname", person.personSurname);
            cmd.Parameters.AddWithValue("identity_code", person.identityCode);
            cmd.ExecuteNonQuery();

            Console.WriteLine($"SAVED {person.personName} INTO PERSON TABLE");
            database.DisposeConnection();
        }

        public static void DeletePerson(int person_id)
        {
            database.OpenConnection();

            string commandText = $"DELETE FROM PERSON WHERE ID = @person_id)";

            using var cmd = new NpgsqlCommand(commandText, database.GetConnection());
            cmd.Parameters.AddWithValue("person_id", person_id);

            cmd.ExecuteNonQuery();

            Console.WriteLine($"DELETED PERSON WITH THE ID OF {person_id} FROM PERSON TABLE");
            database.DisposeConnection();
        }

        public static Person[] UpdatePersonName(int person_id, string person_name)
        {
            int result = 0;
            List<Person> persons = new List<Person>();

            try
            {
                database.OpenConnection();

                string commandText = $"UPDATE PERSON SET person_name = @person_name WHERE person_id = @person_id;";

                using var cmd = new NpgsqlCommand(commandText, database.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);
                cmd.Parameters.AddWithValue("person_name", person_name);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"UPDATED PERSON WITH ID {person_name} IN PERSON TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: "+e.Message);
                Console.WriteLine($"ERROR - Person with ID {person_name} does not exist");
            }
            database.DisposeConnection();
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
