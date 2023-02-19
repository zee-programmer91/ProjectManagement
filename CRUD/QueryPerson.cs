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
                string commandText = $"SELECT * FROM PERSON WHERE DELETED = CAST(0 as bit);";
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
                cmd.Parameters.AddWithValue("identity_code", newPerson.PersonId);
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
            List<Person> persons = new List<Person>();
            //Person person = new Person();
            //person.personId = person_id;
            //person.personName = person_name;
            //person.personSurname = person_surname;

            //string updateQuery = UpdateCreator.CreatePersonUpdateQuery(person);
            //Console.WriteLine("query: " + updateQuery);
            
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        //public static int SoftDeletePerson(int person_id)
        //{
        //    int result = 0;

        //    try
        //    {
        //        string commandText = $"UPDATE PERSON SET deleted = CAST(1 AS bit) WHERE person_id = @person_id;";

        //        using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
        //        cmd.Parameters.AddWithValue("person_id", person_id);

        //        result = cmd.ExecuteNonQuery();
        //        Console.WriteLine($"Soft Deleted Person WITH ID {person_id} IN PERSON TABLE");

        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine($"ERROR - Person with ID {person_id} does not exist");
        //    }
        //    return result;
        //}

        //public static int HardDeletePerson(int person_id)
        //{
        //    int result = 0;

        //    try
        //    {
        //        string commandText = $"DELETE FROM PERSON WHERE person_id = @person_id;";

        //        using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
        //        cmd.Parameters.AddWithValue("person_id", person_id);

        //        result = cmd.ExecuteNonQuery();
        //        Console.WriteLine($"Hard Deleted Person WITH ID {person_id} IN PERSON TABLE\nRows affected: {result}");

        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine($"ERROR - Person with ID {person_id} does not exist");
        //    }
        //    return result;
        //}
    }
}
