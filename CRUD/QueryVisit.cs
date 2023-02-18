using LiveNiceApp;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.CRUD;
using ProjectManagement.utlis;
using Model;

namespace ProjectManagement.Models
{
    public class QueryVisit
    {
        private static readonly DatabaseConnection databaseConnection = new DatabaseConnection();

        public static int GetVisitByID(int visit_id)
        {
            databaseConnection.OpenConnection();
            int result = -1;

            try
            {
                string commandText = $"SELECT * FROM VISIT WHERE visit_id = @visit_id;";
                using NpgsqlCommand sqlCommand = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                sqlCommand.Parameters.AddWithValue("visit_id", visit_id);
                using NpgsqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Visit visit = DatabaseReaders.ReadVisit(reader);
                    Console.WriteLine($"visit: {visit}");
                    databaseConnection.DisposeConnection();
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

            databaseConnection.DisposeConnection();
            return result;
        }

        public static int GetVisits()
        {
            databaseConnection.OpenConnection();
            List<Visit> visits = new List<Visit>();
            int result = -1;

            try
            {
                string commandText = $"SELECT * FROM VISIT;";
                using var sqlCommand = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

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

            databaseConnection.DisposeConnection();
            return result;
        }

        public static int AddVisit(string name, string surname, string identityCode, string email, string cellphone, int tenant_id, DateTime dateOfVisit)
        {
            int result = 0;

            try
            {
                QueryPerson.AddPerson(name, surname, identityCode);

                int person_id = QueryPerson.GetPersonID(name, surname);

                QueryContact.InsertEntry(person_id, new Contact());

                databaseConnection.OpenConnection();
                string commandText = $"INSERT INTO VISIT (person_id, tenant_id, date_of_visit) VALUES(@person_id,@tenant_id, @dateOfVisit);";

                using var sqlCommand = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
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

            databaseConnection.DisposeConnection();
            return result;
        }

        public static int UpdateVisit(int visit_id, DateTime dateOfVisit, DateTime dateLeftVisit)
        {
            databaseConnection.OpenConnection();
            int result = 0;
  
            try
            {
                string commandText = $@"{UpdateCreator.CreateVisitUpdateQuery(dateOfVisit, dateLeftVisit, visit_id)}";

                using var sqlCommand = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"UPDATED VISIT TABLE WHERE ID IS {visit_id}");
                databaseConnection.DisposeConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update VISIT table with ID {visit_id}");
            }

            databaseConnection.DisposeConnection();
            return result;
        }

        public static int UpdateDateLeftVisit(int visit_id, DateTime date)
        {
            databaseConnection.OpenConnection();
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

                using (var sqlCommand = new NpgsqlCommand(commandText, databaseConnection.GetConnection()))
                {
                    sqlCommand.Parameters.AddWithValue("date", date.Date);
                    sqlCommand.Parameters.AddWithValue("visit_id", visit_id);

                    result = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine($"UPDATED the date of the visit with the ID of {visit_id} in VISIT table");
                    databaseConnection.DisposeConnection();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update VISIT table with id {visit_id}");
            }

            databaseConnection.DisposeConnection();
            return result;
        }

        public static int SoftDeleteVisit(int visit_id)
        {
            databaseConnection.OpenConnection();
            int result = 0;
            string commandText = $@"UPDATE VISIT SET deleted = CAST(0 AS BIT) WHERE visit_id = @visit_id;";

            try
            {

                using var sqlCommand = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                sqlCommand.Parameters.AddWithValue("visit_id", visit_id);

                result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"UPDATED VISIT DateFROM WITH ID {visit_id} IN VISIT TABLE");
                databaseConnection.DisposeConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update CONTACT table with id {visit_id}");
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
