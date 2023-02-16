﻿using LiveNiceApp;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.utlis;

namespace ProjectManagement.Models
{
    internal class QueryVisit
    {
        private static readonly DatabaseConnection databaseConnection = new DatabaseConnection();

        public static Visit[] GetVisitByID(int visit_id)
        {
            databaseConnection.OpenConnection();
            List<Visit> visits = new();

            try
            {
                string commandText = $"SELECT * FROM VISIT WHERE visit_id = @visit_id;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("visit_id", visit_id);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Visit visit = DatabaseReaders.ReadVisit(reader);
                    databaseConnection.DisposeConnection();
                    visits.Add(visit);
                    return visits.ToArray();
                }
                Console.WriteLine($"Selected a visit with the ID {visit_id} from the Visit table");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get visit");
            }

            databaseConnection.DisposeConnection();
            return visits.ToArray();
        }

        public static Visit[] GetVisits()
        {
            databaseConnection.OpenConnection();
            List<Visit> visits = new();

            try
            {
                string commandText = $"SELECT * FROM VISIT;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Visit visit = DatabaseReaders.ReadVisit(reader);
                    visits.Add(visit);
                }
                Console.WriteLine($"Selected a visit with the ID from the Visit table");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all visits");
            }

            databaseConnection.DisposeConnection();
            return visits.ToArray();
        }

        public static Visit[] AddVisit(string name, string surname, string identityCode, string email, string cellphone, int tenant_id, DateOnly dateOfVisit)
        {
            try
            {
                QueryPerson.AddPerson(name, surname, identityCode);
                int person_id = QueryPerson.GetPersonID(name, surname);
                QueryContact.AddContact(person_id, cellphone, email);

                databaseConnection.OpenConnection();
                string commandText = $"INSERT INTO VISIT (person_id, tenant_id, date_of_visit) VALUES(@person_id,@tenant_id, @dateOfVisit);";

                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);
                cmd.Parameters.AddWithValue("tenant_id", tenant_id);
                cmd.Parameters.AddWithValue("dateOfVisit", dateOfVisit);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"Saved contact of person with ID {person_id} INTO CONTACT table");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not add visit to the tenant with the following ID: {tenant_id}");
            }

            databaseConnection.DisposeConnection();
            return GetVisits();
        }

        public static Visit[] UpdateDateOfVisit(int visit_id, DateOnly date)
        {
            databaseConnection.OpenConnection();
            List<Visit> visits = new();

            try
            {
                string commandText = $@"UPDATE VISIT SET date_of_visit = @date WHERE visit_id = @visit_id;";

                using (var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("visit_id", visit_id);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"UPDATED VISIT DateFROM WITH ID {visit_id} IN VISIT TABLE");
                    databaseConnection.DisposeConnection();
                    visits.Add(GetVisitByID(visit_id)[0]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update CONTACT table with id {visit_id}");
            }

            databaseConnection.DisposeConnection();
            return visits.ToArray();
        }
    }
}
