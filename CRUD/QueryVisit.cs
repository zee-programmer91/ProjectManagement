using LiveNiceApp;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.CRUD;
using ProjectManagement.utlis;
using Model;
using ProjectManagement.Model;

namespace ProjectManagement.Models
{
    public class QueryVisit
    {
        private static readonly DatabaseConnection databaseConnection = new DatabaseConnection();

        public static int GetVisitByID(int visit_id)
        {
            int result = -1;

            try
            {
                string commandText = $"SELECT * FROM VISIT WHERE visit_id = @visit_id;";
                using NpgsqlCommand sqlCommand = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                sqlCommand.Parameters.AddWithValue("visit_id", visit_id);
                using NpgsqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Visit visit = DatabaseReaders.ReadVisit(reader);
                    Console.WriteLine($"visit: {visit}");
                    result = 1;
                    return result;
                }
                Console.WriteLine($"Selected a visit with the ID {visit_id} from the Visit table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not get visit with ID {visit_id}");
                return result;
            }

            return result;
        }

        public static int GetVisits()
        {
            List<Visit> visits = new List<Visit>();
            int result = -1;

            try
            {
                string commandText = $"SELECT * FROM VISIT;";
                using var sqlCommand = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                using NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Visit visit = DatabaseReaders.ReadVisit(reader);
                    visits.Add(visit);
                }
                result = 1;
                Console.WriteLine($"Selected a visit with the ID from the Visit table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not get all visits");
            }
            return result;
        }

        public static int AddVisit(string name, string surname, string identityCode, string email, string cellphone, int tenant_id, DateTime dateOfVisit)
        {
            int result = 0;

            try
            {
                Person person = new Person()
                {
                    personName = name,
                    personSurname = surname,
                    identityCode = identityCode,
                };
                QueryPerson.InsertEntry(person);

                int person_id = QueryPerson.GetPersonID(name, surname);

                Contact contact = new Contact()
                {
                    personID = person_id,
                    cellphoneNumber = cellphone,
                    email = email,
                };
                QueryContact.InsertEntry(contact);

                string commandText = $"INSERT INTO VISIT (person_id, tenant_id, date_of_visit) VALUES(@person_id,@tenant_id, @dateOfVisit);";

                using var sqlCommand = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                sqlCommand.Parameters.AddWithValue("person_id", person_id);
                sqlCommand.Parameters.AddWithValue("tenant_id", tenant_id);
                sqlCommand.Parameters.AddWithValue("dateOfVisit", dateOfVisit);

                result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"Saved contact of person with ID {person_id} INTO VISIT table");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not add visit to the tenant with the following ID: {tenant_id}");
            }
            return result;
        }

        public static int UpdateVisit(int visit_id, DateTime dateOfVisit, DateTime dateLeftVisit)
        {
            int result = 0;
  
            try
            {
                string commandText = $@"{UpdateCreator.CreateVisitUpdateQuery(dateOfVisit, dateLeftVisit, visit_id)}";

                using var sqlCommand = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"UPDATED VISIT TABLE WHERE ID IS {visit_id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update VISIT table with ID {visit_id}");
            }
            return result;
        }

        public static int UpdateDateLeftVisit(int visit_id, DateTime date)
        {
            int result = 0;

            DateTime dateToday = DateTime.Today;

            if (dateToday.Year == date.Year && dateToday.Day < date.Day) // same year, different day
            {
                return result;
            }
            else if (dateToday.Year < date.Year) // different year
            {
                return result;
            }

            try
            {
                string commandText = $@"UPDATE VISIT SET date_left_visit = @date WHERE visit_id = @visit_id;";

                using (var sqlCommand = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection()))
                {
                    sqlCommand.Parameters.AddWithValue("date", date.Date);
                    sqlCommand.Parameters.AddWithValue("visit_id", visit_id);

                    result = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine($"UPDATED the date of the visit with the ID of {visit_id} in VISIT table");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update VISIT table with id {visit_id}");
            }
            return result;
        }

        public static int SoftDeleteVisit(int visit_id)
        {
            int result = 0;
            string commandText = $@"UPDATE VISIT SET deleted = CAST(0 AS BIT) WHERE visit_id = @visit_id;";

            try
            {

                using var sqlCommand = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                sqlCommand.Parameters.AddWithValue("visit_id", visit_id);

                result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"UPDATED VISIT DateFROM WITH ID {visit_id} IN VISIT TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update CONTACT table with id {visit_id}");
            }
            return result;
        }
    }
}
